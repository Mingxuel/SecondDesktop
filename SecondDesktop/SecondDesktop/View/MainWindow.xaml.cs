using SecondDesktopAppManagerDll;
using SecondDesktopDesktopManagerDll;
using SecondDesktopDll;
using SecondDesktopMessagerDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecondDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VMMain ViewModel = null;
        private bool IsShow = true;
        readonly Dictionary<string, short> hotKeyDic = new Dictionary<string, short>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new VMMain();
            this.DataContext = ViewModel;
            ((VMMain)this.DataContext).SelectPageNotify += SelectPage;
            AppManager.GetInstance().SelectAppNotify += SelectApp;
            SecondDesktopMessager.GetInstance().CreateSubAppNotify += CreateSubApp;
            SelectPage(0);

            var wpfHwnd = new WindowInteropHelper(this).Handle;
            var hWndSource = HwndSource.FromHwnd(wpfHwnd);
            if (hWndSource != null) hWndSource.AddHook(MainWindowProc);

            hotKeyDic.Add("ShowMainWindow", Win32.GlobalAddAtom("ShowMainWindow"));
            hotKeyDic.Add("Alt-D", Win32.GlobalAddAtom("Alt-D"));
            Win32.RegisterHotKey(wpfHwnd, hotKeyDic["ShowMainWindow"], Win32.KeyModifiers.Alt, (int)System.Windows.Forms.Keys.O);
            Win32.RegisterHotKey(wpfHwnd, hotKeyDic["Alt-D"], Win32.KeyModifiers.Alt, (int)System.Windows.Forms.Keys.D);
        }

        private IntPtr MainWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case Win32.WmHotkey:
                    {
                        int sid = wParam.ToInt32();
                        if (sid == hotKeyDic["ShowMainWindow"])
                        {
                            if(IsShow)
                            {
                                Hide();
                                IsShow = false;
                            }
                            else
                            {
                                Show();
                                IsShow = true;
                            }
                        }

                        handled = true;
                        break;
                    }
            }

            return IntPtr.Zero;
        }

        public class Win32
        {
            [DllImport("User32.Dll")]
            public static extern void SetWindowText(int h, String s);

            /// <summary>
            /// 如果函数执行成功，返回值不为0。
            /// 如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。.NET方法:Marshal.GetLastWin32Error()
            /// </summary>
            /// <param name="hWnd">要定义热键的窗口的句柄</param>
            /// <param name="id">定义热键ID（不能与其它ID重复）  </param>
            /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
            /// <param name="vk">定义热键的内容,WinForm中可以使用Keys枚举转换，
            /// WPF中Key枚举是不正确的,应该使用System.Windows.Forms.Keys枚举，或者自定义正确的枚举或int常量</param>
            /// <returns></returns>
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool RegisterHotKey(
                IntPtr hWnd,
                int id,
                KeyModifiers fsModifiers,
                int vk
                );

            /// <summary>
            /// 取消注册热键
            /// </summary>
            /// <param name="hWnd">要取消热键的窗口的句柄</param>
            /// <param name="id">要取消热键的ID</param>
            /// <returns></returns>
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool UnregisterHotKey(
                IntPtr hWnd,
                int id
                );

            /// <summary>
            /// 向全局原子表添加一个字符串，并返回这个字符串的唯一标识符,成功则返回值为新创建的原子ID,失败返回0
            /// </summary>
            /// <param name="lpString"></param>
            /// <returns></returns>
            [DllImport("kernel32", SetLastError = true)]
            public static extern short GlobalAddAtom(string lpString);

            [DllImport("kernel32", SetLastError = true)]
            public static extern short GlobalDeleteAtom(short nAtom);

            /// <summary>
            /// 定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
            /// </summary>
            [Flags()]
            public enum KeyModifiers
            {
                None = 0,
                Alt = 1,
                Ctrl = 2,
                Shift = 4,
                WindowsKey = 8
            }
            /// <summary>
            /// 热键的对应的消息ID
            /// </summary>
            public const int WmHotkey = 0x312;
        }

        private void SelectApp(string AppUID)
        {
            ((VMMain)this.DataContext).AppMainWindowVisibility = Visibility.Visible;
            Assembly assembly = Assembly.LoadFrom(ConfigManager.GetInstance().ApplicationAppsDirectory + AppUID + "\\" + AppUID + ".dll");
            Type factory = assembly.GetType(AppUID + ".Factory");
            MethodInfo method = factory.GetMethod("CreateWindow");
            Object obj = Activator.CreateInstance(factory);
            object[] parameters = new object[] { 0, AppManager.GetInstance().GetConfig(AppUID) };
            UserControl uc = (UserControl)method.Invoke(obj, parameters);
            foreach (var item in AppManager.GetInstance().AppItemList)
            {
                if (item.AppUID == AppUID)
                {
                    ViewModel.AppTitle = item.Name;
                    ViewModel.AppIcon = item.Icon;
                    ViewModel.AppCloseImage = SDResource.DeleteImage;
                    break;
                }
            }
            wpAppPanel.Children.Clear();
            wpAppPanel.Children.Add(uc);
            ViewModel.CloseAppNotify += CloseApp;
        }

        private void CloseApp()
        {
            wpAppPanel.Children.Clear();
            ((VMMain)this.DataContext).AppMainWindowVisibility = Visibility.Hidden;
        }

        public void CreateSubApp(string AppUID, int SubAppUID, string Config)
        {
            DesktopItem item = new DesktopItem();
            item.AppUID = AppUID;
            item.SubAppUID = SubAppUID;
            item.Config = Config;
            item.Page = ((VMMain)this.DataContext).CurrentPage;

            Assembly assembly = Assembly.LoadFrom(AppUID + ".dll");
            Type factory = assembly.GetType(AppUID + ".Factory");
            MethodInfo method = factory.GetMethod("CreateWindow");
            Object obj = Activator.CreateInstance(factory);
            object[] parameters = new object[] { SubAppUID, Config };
            UserControl uc = (UserControl)method.Invoke(obj, parameters);
            SubAppFrame border = new SubAppFrame(item);
            border.Add(uc);
            wpDesktop.Children.Add(border);

            DesktopManager.GetInstance().AddSubApp(item);
        }

        public void OpenSubApp(DesktopItem Item)
        {
            Assembly assembly = Assembly.LoadFrom(Item.AppUID + ".dll");
            Type factory = assembly.GetType(Item.AppUID + ".Factory");
            MethodInfo method = factory.GetMethod("CreateWindow");
            Object obj = Activator.CreateInstance(factory);
            object[] parameters = new object[] { Item.SubAppUID, Item.Config };
            UserControl uc = (UserControl)method.Invoke(obj, parameters);
            SubAppFrame border = new SubAppFrame(Item);
            border.Add(uc);
            wpDesktop.Children.Add(border);
        }

        public void SelectPage(int Page)
        {
            wpDesktop.Children.Clear();
            foreach (DesktopItem item in DesktopManager.GetInstance().DesktopItemList)
            {
                if(item.Page == Page)
                {
                    OpenSubApp(item);
                }
            }
        }
    }
}
