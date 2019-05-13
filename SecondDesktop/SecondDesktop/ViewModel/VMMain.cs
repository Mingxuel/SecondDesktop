using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SecondDesktopDll;
using SecondDesktopAppManagerDll;
using System.Runtime.Serialization;
using SecondDesktopDesktopManagerDll;
using SecondDesktopMessagerDll;
using System.Windows.Media.Imaging;
using SecondDesktopHotkeyManagerDll;
using System.Windows.Media.Animation;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;

namespace SecondDesktop
{
    class VMMain : NotifyObject
    {
        public delegate void CloseAppDelegate();
        public event CloseAppDelegate CloseAppNotify;

        private bool IsShow = true;
        public IEnumerable<Swatch> Swatches { get; }
        public MMain Model { get; set; }
        public VMMain()
        {
            Model = new MMain();
			AppWindowVisibility = Visibility.Hidden;
            MainWindowWidth = SDSystem.WindowWidth;

            HotkeyManager.GetInstance().RegisterHotKey("ShowWindow", LKey.None, RKey.F2, ShowWindow);

            Swatches = new SwatchesProvider().Swatches;

			Window win = Application.Current.MainWindow;
			PresentationSource source = PresentationSource.FromVisual(win);
			Matrix matrix = source.CompositionTarget.TransformFromDevice;
			double widthRatio = matrix.M11;
			double heightRatio = matrix.M22;
			double screenWidth = SystemParameters.PrimaryScreenWidth * widthRatio;
			double screenHeight = SystemParameters.PrimaryScreenHeight * heightRatio;
			MainWindowHeight = SystemParameters.WorkArea.Size.Height * heightRatio;
			MainWindowLeft = screenWidth - MainWindowWidth;

			MessageBoxMessage = "Are you sure delete this desktop page?";

			DesktopWindowManager.GetInstance().ThemeDark = DesktopWindowManager.GetInstance().ThemeDark;
			DesktopWindowManager.GetInstance().ThemeColor = DesktopWindowManager.GetInstance().ThemeColor;
            IsThemeDark = DesktopWindowManager.GetInstance().ThemeDark;
        }

        private int ShowWindow()
        {
            if (!IsShow)
            {
                DoubleAnimation opacityAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)), FillBehavior.HoldEnd);
                Application.Current.MainWindow.BeginAnimation(Window.OpacityProperty, opacityAnimation);
                IsShow = true;
            }
            else
            {
                DoubleAnimation opacityAnimation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.3)), FillBehavior.HoldEnd);
                Application.Current.MainWindow.BeginAnimation(Window.OpacityProperty, opacityAnimation);
                IsShow = false;
            }

            return 0;
        }

		#region About MainWindow
		public double MainWindowWidth
		{
			get { return Model.MainWindowWidth; }
			set
			{
				Model.MainWindowWidth = value;
				RaisePropertyChanged("MainWindowWidth");
			}
		}

		public double MainWindowLeft
		{
			get { return Model.MainWindowLeft; }
			set
			{
				Model.MainWindowLeft = value;
				RaisePropertyChanged("MainWindowLeft");
			}
		}

		public double MainWindowHeight
		{
			get { return Model.MainWindowHeight; }
			set
			{
				Model.MainWindowHeight = value;
				RaisePropertyChanged("MainWindowHeight");
			}
		}
		#endregion

		#region About AppWindow
		public Visibility AppWindowVisibility
		{
			get { return Model.AppWindowVisibility; }
			set
			{
				Model.AppWindowVisibility = value;
				RaisePropertyChanged("AppWindowVisibility");
			}
		}

		public string AppIcon
		{
			get { return Model.AppIcon; }
			set
			{
				Model.AppIcon = value;
				RaisePropertyChanged("AppIcon");
			}
		}

		public string AppTitle
		{
			get { return Model.AppTitle; }
			set
			{
				Model.AppTitle = value;
				RaisePropertyChanged("AppTitle");
			}
		}

		private SDCommand<string> appCloseClickCommand;
		public SDCommand<string> AppCloseClickCommand
		{
			get
			{
				if (appCloseClickCommand == null)
					appCloseClickCommand = new SDCommand<string>(
						new Action<string>(e =>
						{
							CloseAppNotify();
						}), null);
				return appCloseClickCommand;
			}
		}
		#endregion

		#region About Apps
		public double BottomDrawerHeight
		{
			get { return Model.BottomDrawerHeight; }
			set
			{
				Model.BottomDrawerHeight = value;
				RaisePropertyChanged("BottomDrawerHeight");
			}
		}

		public Visibility AppsVisibility
		{
			get { return Model.AppsVisibility; }
			set
			{
				Model.AppsVisibility = value;
				RaisePropertyChanged("AppsVisibility");
			}
		}

		public Visibility ThemeVisibility
		{
			get { return Model.ThemeVisibility; }
			set
			{
				Model.ThemeVisibility = value;
				RaisePropertyChanged("ThemeVisibility");
			}
		}

		private bool IsAppsSettings = false;
		private SDCommand<string> appsSettingsCommand;
		public SDCommand<string> AppsSettingsCommand
		{
			get
			{
				if (appsSettingsCommand == null)
					appsSettingsCommand = new SDCommand<string>(
						new Action<string>(e =>
						{
							IsAppsSettings = !IsAppsSettings;
							AppManager.GetInstance().SettingApp(IsAppsSettings);
						}), null);
				return appsSettingsCommand;
			}
		}

		private SDCommand<bool> themeLightDarkCommand;
		public SDCommand<bool> ThemeLightDarkCommand
		{
			get
			{
				if (themeLightDarkCommand == null)
					themeLightDarkCommand = new SDCommand<bool>(
						new Action<bool>(e =>
						{
							DesktopWindowManager.GetInstance().ThemeDark = e;
						}), null);
				return themeLightDarkCommand;
			}
		}

		private SDCommand<Swatch> themeColorCommand;
		public SDCommand<Swatch> ThemeColorCommand
		{
			get
			{
				if (themeColorCommand == null)
					themeColorCommand = new SDCommand<Swatch>(
						new Action<Swatch>(e =>
						{
							DesktopWindowManager.GetInstance().ThemeColor = e;
						}), null);
				return themeColorCommand;
			}
		}
        #endregion

        #region About Desktop
        public bool IsThemeDark
        {
            get { return Model.IsThemeDark; }
            set
            {
                Model.IsThemeDark = value;
                RaisePropertyChanged("IsThemeDark");
            }
        }

        private bool isAppWindowOpen;
        public bool IsAppWindowOpen
        {
            get { return isAppWindowOpen; }
            set
            {
                if (isAppWindowOpen == value) return;
                isAppWindowOpen = value;
                RaisePropertyChanged("IsAppWindowOpen");
            }
        }

        private object appWindowContent;
        public object AppWindowContent
        {
            get { return appWindowContent; }
            set
            {
                if (appWindowContent == value) return;
                appWindowContent = value;
                RaisePropertyChanged("AppWindowContent");
            }
        }

		private SDCommand<string> settingsDesktopCommand;
		public SDCommand<string> SettingsDesktopCommand
		{
			get
			{
				if (settingsDesktopCommand == null)
					settingsDesktopCommand = new SDCommand<string>(
						new Action<string>(e =>
						{
                            DesktopWindowManager.GetInstance().SettingsDesktop();
						}), null);
				return settingsDesktopCommand;
			}
		}

		private SDCommand<string> addDesktopCommand;
		public SDCommand<string> AddDesktopCommand
		{
			get
			{
				if (addDesktopCommand == null)
					addDesktopCommand = new SDCommand<string>(
						new Action<string>(e =>
						{
							DesktopWindowManager.GetInstance().AddDesktop();
						}), null);
				return addDesktopCommand;
			}
		}

		private SDCommand<string> deleteDesktopCommand;
		public SDCommand<string> DeleteDesktopCommand
		{
			get
			{
				if (deleteDesktopCommand == null)
					deleteDesktopCommand = new SDCommand<string>(
						new Action<string>(e =>
						{
							DesktopWindowManager.GetInstance().DeleteDesktop();
						}), null);
				return deleteDesktopCommand;
			}
		}

		public string MessageBoxMessage
		{
			get { return Model.MessageBoxMessage; }
			set
			{
				Model.MessageBoxMessage = value;
				RaisePropertyChanged("MessageBoxMessage");
			}
		}
		#endregion
	}
}
