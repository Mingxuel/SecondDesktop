using SecondDesktopAppManagerDll;
using SecondDesktopDesktopManagerDll;
using SecondDesktopDll;
using SecondDesktopHotkeyManagerDll;
using SecondDesktopMessagerDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SecondDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Progress ProgressBar = null;
        bool IsLoadComplete = false;
        VMMain ViewModel = null;
        public MainWindow()
        {
            InitializeComponent();
            ShowInTaskbar = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new VMMain();
            this.DataContext = ViewModel;
            AppManager.GetInstance().SelectAppNotify += SelectApp;
            HotkeyManager.GetInstance().StartListen();
        }

        private void SelectApp(string AppUID)
        {
            ViewModel.AppWindowVisibility = Visibility.Visible;
            ProgressBar = new Progress();
            ViewModel.AppWindowContent = ProgressBar;
            ViewModel.IsAppWindowOpen = true;

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (s, f) =>
            {
                Thread.Sleep(500);
            };

            bw.RunWorkerCompleted += (s, f) =>
            {
                Application.Current.Dispatcher.BeginInvoke((Action)delegate {
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
                            break;
                        }
                    }
                    ViewModel.IsAppWindowOpen = false;
                    ViewModel.AppWindowContent = null;
                    ViewModel.IsAppWindowOpen = true;
                    ViewModel.AppWindowContent = uc;
                });
            };

            bw.RunWorkerAsync();
        }

        private void CloseApp()
        {
            wpAppPanel.Children.Clear();
            ((VMMain)this.DataContext).AppWindowVisibility = Visibility.Hidden;
        }
 
        private void Window_LostFocus(object sender, RoutedEventArgs e)
		{
			Topmost = true;
		}

		private void AppsClick(object sender, RoutedEventArgs e)
		{
			ViewModel.ThemeVisibility = Visibility.Hidden;
			ViewModel.AppsVisibility = Visibility.Visible;
			ViewModel.BottomDrawerHeight = 156;
		}

		private void ThemeClick(object sender, RoutedEventArgs e)
		{
			ViewModel.ThemeVisibility = Visibility.Visible;
			ViewModel.AppsVisibility = Visibility.Hidden;
			ViewModel.BottomDrawerHeight = 124;
		}
	}
}
