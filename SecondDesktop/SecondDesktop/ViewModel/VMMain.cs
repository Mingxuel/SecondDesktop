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
        private readonly DialogHost _dialogHost;

        public delegate void SelectPageDelegate(int Page);
        public event SelectPageDelegate SelectPageNotify;

        public delegate void CloseAppDelegate();
        public event CloseAppDelegate CloseAppNotify;
        private Func<bool, int> MassageBoxReturn = null;

        private bool IsShow = true;
        public IEnumerable<Swatch> Swatches { get; }
        public MMain Model { get; set; }
        public VMMain()
        {
            Model = new MMain();
            AppMainWindowVisibility = Visibility.Hidden;
            MessageBoxVisibility = Visibility.Hidden;
            MainWindowWidth = SDSystem.WindowWidth;

            HotkeyManager.GetInstance().RegisterHotKey("ShowWindow", LKey.None, RKey.F2, ShowWindow);

            SecondDesktopMessager.GetInstance().ShowMessageBoxNotify += ShowMessageBox;

            Swatches = new SwatchesProvider().Swatches;

            CalculateScreenSize();

            _dialogHost = new DialogHost();
            _dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
            _dialogHost.IsOpen = true;
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

        int i = 0;

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

        public Visibility MessageBoxVisibility
        {
            get { return Model.MessageBoxVisibility; }
            set
            {
                Model.MessageBoxVisibility = value;
                RaisePropertyChanged("MessageBoxVisibility");
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

        private SDCommand<string> messageBoxYesClickCommand;
        public SDCommand<string> MessageBoxYesClickCommand
        {
            get
            {
                if (messageBoxYesClickCommand == null)
                    messageBoxYesClickCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            if(MassageBoxReturn != null)
                            {
                                MassageBoxReturn(true);
                                MassageBoxReturn = null;
                            }
                            MessageBoxVisibility = Visibility.Hidden;
                        }), null);
                return messageBoxYesClickCommand;
            }
        }

        private SDCommand<string> messageBoxNoClickCommand;
        public SDCommand<string> MessageBoxNoClickCommand
        {
            get
            {
                if (messageBoxNoClickCommand == null)
                    messageBoxNoClickCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            if (MassageBoxReturn != null)
                            {
                                MassageBoxReturn(false);
                                MassageBoxReturn = null;
                            }
                            MessageBoxVisibility = Visibility.Hidden;
                        }), null);
                return messageBoxNoClickCommand;
            }
        }

        private async void ShowMessageBox(string pMessage, Func<bool, int> pReturn)
        {
            var view = new MessageBoxYesNo
            {
                DataContext = new VMMessageBoxYesNo()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));

            //MessageBoxMessage = pMessage;
            //MessageBoxVisibility = Visibility.Visible;
            //MassageBoxReturn = pReturn;
        }

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
            Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");
        }

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new MessageBoxYesNo());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());
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
                            new PaletteHelper().SetLightDark(e);
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
                            new PaletteHelper().ReplacePrimaryColor(e);
                            new PaletteHelper().ReplaceAccentColor(e);
                        }), null);
                return themeColorCommand;
            }
        }
    }
}
