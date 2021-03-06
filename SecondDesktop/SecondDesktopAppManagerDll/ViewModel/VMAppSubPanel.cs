﻿using SecondDesktopDll;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SecondDesktopAppManagerDll
{
    [DataContract]
    class VMAppSubPanel : NotifyObject
    {
        private MAppSubPanel Model = null;
        private int SelectPage = 0;

        public VMAppSubPanel()
        {
            Model = new MAppSubPanel();
            Init();
        }

        public void Init()
        {
            DeleteVisibility = Visibility.Hidden;
            AppManager.GetInstance().Update += Update;
            AppManager.GetInstance().SettingAppNotify += SettingApp;
            Update();
        }

        [DataMember]
        public ObservableCollection<AppItem> AppItems
        {
            get
            {
                return Model.AppItems;
            }
            set
            {
                Model.AppItems = value;
                RaisePropertyChanged("AppItems");
            }
        }

        [DataMember]
        public ObservableCollection<AppPageItem> PageItems
        {
            get
            {
                return Model.PageItems;
            }
            set
            {
                Model.PageItems = value;
                RaisePropertyChanged("PageItems");
            }
        }

        public Visibility DeleteVisibility
        {
            get
            {
                return Model.DeleteVisibility;
            }
            set
            {
                Model.DeleteVisibility = value;
                RaisePropertyChanged("DeleteVisibility");
            }
        }

        private string selectAppUID = "";
		private SDCommand<string> appClickCommand;
        public SDCommand<string> AppClickCommand
        {
            get
            {
                if (appClickCommand == null)
                    appClickCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            selectAppUID = e;
                            SelectApp();
                        }), null);
                return appClickCommand;
            }
        }

        private void SelectApp()
        {
            AppManager.GetInstance().SelectApp(selectAppUID);
        }

        private SDCommand<string> appDeleteCommand;
        public SDCommand<string> AppDeleteCommand
        {
            get
            {
                if (appDeleteCommand == null)
                    appDeleteCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            if(e != "SecondDesktopAppStore")
                                AppManager.GetInstance().DeleteApp(e);
                        }), null);
                return appDeleteCommand;
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
                            SelectPage = e;
                            Update();
                        }), null);
                return pageClickCommand;
            }
        }

        public void Update()
        {
            PageItems.Clear();
            foreach (var item in AppManager.GetInstance().PageList)
            {
                AppPageItem pageItem = new AppPageItem();
                pageItem.Page = item.Key;
                pageItem.IsSelect = false;
                PageItems.Add(pageItem);
            }

            bool isExist = false;
            foreach (AppPageItem item in PageItems)
            {
                if(item.Page == SelectPage)
                {
                    item.IsSelect = true;
                    isExist = true;
                    break;
                }
            }

            if (!isExist)
            {
                SelectPage = 0;
                foreach (AppPageItem item in PageItems)
                {
                    if (item.Page == SelectPage)
                    {
                        item.IsSelect = true;
                        break;
                    }
                }
            }

            AppItems.Clear();
            foreach(AppItem item in AppManager.GetInstance().AppItemList)
            {
                if(item.Page == SelectPage)
                {
                    AppItems.Add(item);
                }
            }
        }

        public void SettingApp(bool IsSetting)
        {
            if (IsSetting)
            {
                DeleteVisibility = Visibility.Visible;
            }
            else
            {
                DeleteVisibility = Visibility.Hidden;
            }
        }
    }
}
