﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using SecondDesktopDll;
using SecondDesktopAppManagerDll;
using System.Runtime.Serialization;

namespace SecondDesktopAppStore
{
	[DataContract]
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
            AppItems.Clear();

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

				appStoreItem.AppUID = appItem.AppUID;
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

		[DataMember]
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

		private SDCommand<string> activeCommand;
		public SDCommand<string> ActiveCommand
		{
			get
			{
				if (activeCommand == null)
					activeCommand = new SDCommand<string>(
						new Action<string>(e =>
						{
							if(!AppManager.GetInstance().ExistsApp(e))
							{
								AppManager.GetInstance().AddApp(ConfigManager.GetInstance().ApplicationAppsDirectory + e);
							} else
                            {
                                AppManager.GetInstance().DeleteApp(e);
                            }
                            Init();
						}), null);
				return activeCommand;
			}
		}

		private SDCommand<string> importCommand;
		public SDCommand<string> ImportCommand
		{
			get
			{
				if (importCommand == null)
					importCommand = new SDCommand<string>(
						new Action<string>(e =>
						{

						}), null);
				return importCommand;
			}
		}
	}
}
