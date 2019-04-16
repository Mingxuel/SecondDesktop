using SecondDesktopDll;
using SecondDesktopMessagerDll;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SecondDesktopDesktopManagerDll
{
    class VMDesktopPanel : NotifyObject
    {
        public delegate void SelectPageDelegate(int Page);
        public event SelectPageDelegate SelectPageNotify;

        public delegate void CloseAppDelegate();
        public event CloseAppDelegate CloseAppNotify;

        public MDesktopPanel Model { get; set; }
        public VMDesktopPanel()
        {
            Model = new MDesktopPanel();
            Width = SDSystem.WindowWidth - SDSystem.WindowMargin*2;
            DesktopTitleReadOnly = true;
            DesktopTitleBackground = "#2D2D2D";
            DesktopImage = SDResource.DesktopImage;
            DesktopSettingImage = SDResource.SettingsImage;
            DesktopAddImage = SDResource.AddImage;
            DesktopDeleteImage = SDResource.DeleteImage;

            DesktopDataManager.GetInstance().Update += Update;
            Update();
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

        public int CurrentPage
        {
            get { return DesktopWindowManager.GetInstance().CurrentPage; }
            set
            {
				DesktopWindowManager.GetInstance().CurrentPage = value;
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

        public string DesktopImage
        {
            get { return Model.DesktopImage; }
            set
            {
                Model.DesktopImage = value;
                RaisePropertyChanged("DesktopImage");
            }
        }

        public string DesktopSettingImage
        {
            get { return Model.DesktopSettingImage; }
            set
            {
                Model.DesktopSettingImage = value;
                RaisePropertyChanged("DesktopSettingImage");
            }
        }

        public string DesktopAddImage
        {
            get { return Model.DesktopAddImage; }
            set
            {
                Model.DesktopAddImage = value;
                RaisePropertyChanged("DesktopAddImage");
            }
        }

        public string DesktopDeleteImage
        {
            get { return Model.DesktopDeleteImage; }
            set
            {
                Model.DesktopDeleteImage = value;
                RaisePropertyChanged("DesktopDeleteImage");
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
                            SecondDesktopMessager.GetInstance().DesktopSettings(!DesktopTitleReadOnly);
                            Update();
                        }), null);
                return pageClickCommand;
            }
        }

        private SDCommand<string> saveTitleCommand;
        public SDCommand<string> SaveTitleCommand
        {
            get
            {
                if (saveTitleCommand == null)
                    saveTitleCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            DesktopDataManager.GetInstance().ModifyPage(CurrentPage, DesktopTitle);
                        }), null);
                return saveTitleCommand;
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
                            if (DesktopTitleReadOnly)
                            {
                                DesktopTitleReadOnly = false;
                                DesktopTitleBackground = "#7D7D7D";
                                DesktopSettingImage = SDResource.SaveImage;
                                SecondDesktopMessager.GetInstance().DesktopSettings(true);
                            }
                            else
                            {
                                DesktopTitleReadOnly = true;
                                DesktopTitleBackground = "#2D2D2D";
                                DesktopSettingImage = SDResource.SettingsImage;
                                DesktopDataManager.GetInstance().ModifyPage(CurrentPage, DesktopTitle);
                                SecondDesktopMessager.GetInstance().DesktopSettings(false);
                            }
                        }), null);
                return settingsClickCommand;
            }
        }

        private int CloseDesktop(bool pStatus)
        {
            if(pStatus)
            {
                DesktopDataManager.GetInstance().DeletePage(CurrentPage);
                CurrentPage = 0;
                Update();
            }

            return 0;
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

        public void Update()
        {
            DesktopPageItems.Clear();
            foreach (var item in DesktopDataManager.GetInstance().PageList)
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

            if (SelectPageNotify != null)
            {
                SelectPageNotify(CurrentPage);
            }

            if (DesktopDataManager.GetInstance().PageList.Count() > 0)
            {
                DesktopTitle = DesktopDataManager.GetInstance().PageList[CurrentPage];
            }
        }
    }
}
