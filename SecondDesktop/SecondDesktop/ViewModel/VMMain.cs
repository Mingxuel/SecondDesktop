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

namespace SecondDesktop
{
    class VMMain : NotifyObject
    {
        public delegate void SelectPageDelegate(int Page);
        public event SelectPageDelegate SelectPageNotify;

        public delegate void CloseAppDelegate();
        public event CloseAppDelegate CloseAppNotify;

        public MMain Model { get; set; }
        public VMMain()
        {
            Model = new MMain();
            AppMainWindowVisibility = Visibility.Hidden;
            MainWindowWidth = SDSystem.WindowWidth;
            CalculateScreenSize();
        }

        private void CalculateScreenSize()
        {
            Window win = Application.Current.MainWindow;
            PresentationSource source = PresentationSource.FromVisual(win);
            Matrix matrix = source.CompositionTarget.TransformFromDevice;
            double widthRatio = matrix.M11;
            double heightRatio = matrix.M22;
            double screenWidth = SystemParameters.PrimaryScreenWidth * widthRatio;
            double screenHeight = SystemParameters.PrimaryScreenHeight * heightRatio;
            MainWindowHeight = SystemParameters.WorkArea.Size.Height * heightRatio;
            MainWindowLeft = screenWidth - MainWindowWidth;
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get
            {
                return _loadedCommand;
            }
            set
            {
                _loadedCommand = value;
            }
        }

        public double MainWindowWidth
        {
            get { return Model.MainWindowWidth; }
            set
            {
                Model.MainWindowWidth = value;
                RaisePropertyChanged("MainWindowWidth");
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

        public double MainWindowLeft
        {
            get { return Model.MainWindowLeft; }
            set
            {
                Model.MainWindowLeft = value;
                RaisePropertyChanged("MainWindowLeft");
            }
        }

        public Visibility AppMainWindowVisibility
        {
            get { return Model.AppMainWindowVisibility; }
            set
            {
                Model.AppMainWindowVisibility = value;
                RaisePropertyChanged("AppMainWindowVisibility");
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

        public string AppIcon
        {
            get { return Model.AppIcon; }
            set
            {
                Model.AppIcon = value;
                RaisePropertyChanged("AppIcon");
            }
        }

        public string AppCloseImage
        {
            get { return Model.AppCloseImage; }
            set
            {
                Model.AppCloseImage = value;
                RaisePropertyChanged("AppCloseImage");
            }
        }
    }
}
