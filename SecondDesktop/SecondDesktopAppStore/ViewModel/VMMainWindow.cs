using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using SecondDesktopDll;
using SecondDesktopAppManagerDll;

namespace SecondDesktopAppStore
{
    class VMMainWindow : NotifyObject
    {
        public MMainWindow Model { get; set; }
        public VMMainWindow()
        {
			Model = new MMainWindow();

            Init();
        }

        private void Init()
        {
            string[] appsPath = Directory.GetDirectories(ConfigManager.GetInstance().ApplicationAppsDirectory);

            foreach(string path in appsPath)
            {
                bool exist = false;
                AppStoreItem appStoreItem = new AppStoreItem();
                AppItem appItem = AppManager.GetInstance().LoadApp(path);
                foreach(var item in AppManager.GetInstance().AppItemList)
                {
                    if(item.AppUID == appItem.AppUID)
                    {
                        exist = true;
                        break;
                    }
                }

                appStoreItem.Icon = appItem.Icon;
                appStoreItem.Title = appItem.Name;
                if (exist)
                {
                    appStoreItem.Status = "UnInstall";
                } else
                {
                    appStoreItem.Status = "Install";
                }
                AppItems.Add(appStoreItem);
            }
        }

        public ObservableCollection<AppStoreItem> AppItems
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
    }
}
