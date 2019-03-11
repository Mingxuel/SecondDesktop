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
using System.Windows.Media.Imaging;

namespace SecondDesktop
{
    class VMMain : NotifyObject
    {
        public delegate void SelectPageDelegate(int Page);
        public event SelectPageDelegate SelectPageNotify;

        public MMain Model { get; set; }
        public VMMain()
        {
            Model = new MMain();
            AppMainWindowVisibility = Visibility.Hidden;
            Width = SDSystem.WindowWidth;
            CalculateScreenSize();
            CurrentPage = 0;
            DesktopTitleReadOnly = true;
            DesktopTitleBackground = "#2D2D2D";
			DesktopImage = SDResource.DesktopImage;
			DesktopSettingImage = SDResource.SettingsImage;
			DesktopAddImage = SDResource.AddImage;

			LoadedCommand = new RelayCommand(new Action<object>(Loaded));
            Update();
        }

        private void CalculateScreenSize()
        {
            Window win = Application.Current.MainWindow;

            PresentationSource source = PresentationSource.FromVisual(win);

            Matrix m = source.CompositionTarget.TransformFromDevice;

            double widthRatio = m.M11;

            double heightRatio = m.M22;

            double screenWidth = SystemParameters.PrimaryScreenWidth * widthRatio;

            double screenHeight = SystemParameters.PrimaryScreenHeight * heightRatio;

            Height = SystemParameters.WorkArea.Size.Height * heightRatio;
            Left = screenWidth - Width;
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

        public void Loaded(object obj)
        {

        }

        public double Width
        {
            get { return Model.Width; }
            set
            {
                Model.Width = value;
                RaisePropertyChanged("Width");
            }
        }

        public double Height
        {
            get { return Model.Height; }
            set
            {
                Model.Height = value;
                RaisePropertyChanged("Height");
            }
        }

        public double Left
        {
            get { return Model.Left; }
            set
            {
                Model.Left = value;
                RaisePropertyChanged("Left");
            }
        }

        public double Top
        {
            get { return Model.Top; }
            set
            {
                Model.Top = value;
                RaisePropertyChanged("Top");
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

        public int CurrentPage
        {
            get { return Model.CurrentPage; }
            set
            {
                Model.CurrentPage = value;
                RaisePropertyChanged("CurrentPage");
            }
        }

        public string DesktopTitle
        {
            get { return Model.DesktopTitle; }
            set
            {
                Model.DesktopTitle = value;
                RaisePropertyChanged("DesktopTitle");
            }
        }


        public string DesktopTitleBackground
        {
            get { return Model.DesktopTitleBackground; }
            set
            {
                Model.DesktopTitleBackground = value;
                RaisePropertyChanged("DesktopTitleBackground");
            }
        }
        public bool DesktopTitleReadOnly
        {
            get { return Model.DesktopTitleReadOnly; }
            set
            {
                Model.DesktopTitleReadOnly = value;
                RaisePropertyChanged("DesktopTitleReadOnly");
            }
        }

		public BitmapImage DesktopImage
		{
			get { return Model.DesktopImage; }
			set
			{
				Model.DesktopImage = value;
				RaisePropertyChanged("DesktopImage");
			}
		}

		public BitmapImage DesktopSettingImage
		{
			get { return Model.DesktopSettingImage; }
			set
			{
				Model.DesktopSettingImage = value;
				RaisePropertyChanged("DesktopSettingImage");
			}
		}

		public BitmapImage DesktopAddImage
		{
			get { return Model.DesktopAddImage; }
			set
			{
				Model.DesktopAddImage = value;
				RaisePropertyChanged("DesktopAddImage");
			}
		}

		[DataMember]
        public ObservableCollection<DesktopItem> DesktopItems
        {
            get
            {
                return Model.DesktopItems;
            }
            set
            {
                Model.DesktopItems = value;
                RaisePropertyChanged("DesktopItems");
            }
        }

        [DataMember]
        public ObservableCollection<DesktopPageItem> DesktopPageItems
        {
            get
            {
                return Model.DesktopPageItems;
            }
            set
            {
                Model.DesktopPageItems = value;
                RaisePropertyChanged("DesktopPageItems");
            }
        }

		private SDCommand<int> pageClickCommand;
        public SDCommand<int> PageClickCommand
        {
            get
            {
                if (pageClickCommand == null)
                    pageClickCommand = new SDCommand<int>(
                        new Action<int>(e =>
                        {
                            CurrentPage = e;
                            Update();
                        }), null);
                return pageClickCommand;
            }
        }

        private SDCommand<string> settingsClickCommand;
        public SDCommand<string> SettingsClickCommand
        {
            get
            {
                if (settingsClickCommand == null)
                    settingsClickCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            if(DesktopTitleReadOnly)
                            {
                                DesktopTitleReadOnly = false;
                                DesktopTitleBackground = "#7D7D7D";
								DesktopSettingImage = SDResource.SaveImage;
                            }
                            else
                            {
                                DesktopTitleReadOnly = true;
                                DesktopTitleBackground = "#2D2D2D";
								DesktopSettingImage = SDResource.SettingsImage;
								DesktopManager.GetInstance().ModifyPage(CurrentPage, DesktopTitle);
                            }
                        }), null);
                return settingsClickCommand;
            }
        }

        private SDCommand<string> addClickCommand;
        public SDCommand<string> AddClickCommand
        {
            get
            {
                if (addClickCommand == null)
                    addClickCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            int page = 0;
                            foreach (var item in DesktopManager.GetInstance().PageList)
                            {
                                if(page <= item.Key)
                                {
                                    page = item.Key + 1;
                                }
                            }

                            DesktopManager.GetInstance().ModifyPage(page, "DESKTOP");
                            CurrentPage = page;
                            Update();
                        }), null);
                return addClickCommand;
            }
        }

        public void Update()
        {
            DesktopPageItems.Clear();
            foreach (var item in DesktopManager.GetInstance().PageList)
            {
                DesktopPageItem pageItem = new DesktopPageItem();
                pageItem.Page = item.Key;
                pageItem.Title = item.Value;
                pageItem.IsSelect = false;
                DesktopPageItems.Add(pageItem);
            }

            bool isExist = false;
            foreach (DesktopPageItem item in DesktopPageItems)
            {
                if (item.Page == CurrentPage)
                {
                    item.IsSelect = true;
                    isExist = true;
                    break;
                }
            }

            if (!isExist)
            {
                CurrentPage = 0;
                foreach (DesktopPageItem item in DesktopPageItems)
                {
                    if (item.Page == CurrentPage)
                    {
                        item.IsSelect = true;
                        break;
                    }
                }
            }

            if (SelectPageNotify != null) {
                SelectPageNotify(CurrentPage);
            }

			if(DesktopManager.GetInstance().PageList.Count() > 0)
			{
				DesktopTitle = DesktopManager.GetInstance().PageList[CurrentPage];
			}
        }
    }
}
