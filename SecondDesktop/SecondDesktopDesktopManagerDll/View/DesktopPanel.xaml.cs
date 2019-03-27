using SecondDesktopAppManagerDll;
using SecondDesktopDll;
using SecondDesktopMessagerDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecondDesktopDesktopManagerDll
{
    /// <summary>
    /// Interaction logic for DesktopPanel.xaml
    /// </summary>
    public partial class DesktopPanel : UserControl
    {
        VMDesktopPanel ViewModel = null;
        public DesktopPanel()
        {
            InitializeComponent();
            ViewModel = new VMDesktopPanel();
            DataContext = ViewModel;

            ViewModel.SelectPageNotify += SelectPage;
            SecondDesktopMessager.GetInstance().CreateSubAppNotify += CreateSubApp;
            SelectPage(0);
        }

        public void CreateSubApp(string AppUID, int SubAppUID, string Config)
        {
            DesktopItem item = new DesktopItem();
            item.AppUID = AppUID;
            item.SubAppUID = SubAppUID;
            item.Config = Config;
            item.Page = ViewModel.CurrentPage;

            Assembly assembly = Assembly.LoadFrom(ConfigManager.GetInstance().ApplicationAppsDirectory + AppUID + "\\" + AppUID + ".dll");
            Type factory = assembly.GetType(AppUID + ".Factory");
            MethodInfo method = factory.GetMethod("CreateWindow");
            Object obj = Activator.CreateInstance(factory);
            object[] parameters = new object[] { SubAppUID, Config };
            UserControl uc = (UserControl)method.Invoke(obj, parameters);
            SubAppFrame border = new SubAppFrame(item);
            SecondDesktopMessager.GetInstance().DesktopSettings(!ViewModel.DesktopTitleReadOnly);
            border.Add(uc);
            wpDesktop.Children.Add(border);

            DesktopDataManager.GetInstance().AddSubApp(item);
        }

        public void OpenSubApp(DesktopItem Item)
        {
            Assembly assembly = Assembly.LoadFrom(ConfigManager.GetInstance().ApplicationAppsDirectory + Item.AppUID + "\\" + Item.AppUID + ".dll");
            Type factory = assembly.GetType(Item.AppUID + ".Factory");
            MethodInfo method = factory.GetMethod("CreateWindow");
            Object obj = Activator.CreateInstance(factory);
            object[] parameters = new object[] { Item.SubAppUID, Item.Config };
            UserControl uc = (UserControl)method.Invoke(obj, parameters);
            SubAppFrame border = new SubAppFrame(Item);
            SecondDesktopMessager.GetInstance().DesktopSettings(!ViewModel.DesktopTitleReadOnly);
            border.Add(uc);
            wpDesktop.Children.Add(border);
        }

        public void SelectPage(int Page)
        {
            wpDesktop.Children.Clear();
            foreach (DesktopItem item in DesktopDataManager.GetInstance().DesktopItemList)
            {
                if (item.Page == Page)
                {
                    OpenSubApp(item);
                }
            }
        }
    }
}
