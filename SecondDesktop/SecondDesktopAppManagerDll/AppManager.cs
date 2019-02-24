using SecondDesktopDll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktopAppManagerDll
{
    public class AppManager
    {
        public delegate void UpdateDelegate();
        public event UpdateDelegate Update;

        public delegate void SelectAppNotifyDelegate(string AppUID);
        public event SelectAppNotifyDelegate SelectAppNotify;

        public List<AppItem> appItemList = null;
        public List<AppItem> AppItemList {
            get
            {
                if(appItemList == null)
                {
                    appItemList = new List<AppItem>();
                }
                return appItemList;
            }
            set
            {
                appItemList = value;
            }
        }

        public Dictionary<int, int> pageList = null;
        public Dictionary<int, int> PageList
        {
            get
            {
                if (pageList == null)
                {
                    pageList = new Dictionary<int, int>();
                }
                return pageList;
            }
            set
            {
                pageList = value;
            }
        }

        private static string ConfigPath = "";
        private static AppManager appManager = null;
        public static AppManager GetInstance()
        {
            if(appManager == null)
            {
                appManager = Init();
            }

            return appManager;
        }

        private static AppManager Init()
        {
            AppManager manager = null;
            ConfigPath = ConfigManager.GetInstance().SystemConfigDirectory + "app.config";
            if (!File.Exists(ConfigPath))
            {
                FileStream stream = File.Create(ConfigPath);
                stream.Close();
                
                File.WriteAllText(ConfigPath, MD5.Encrypt("[]"));
            }

            string json = File.ReadAllText(ConfigPath);
            json = MD5.Decrypt(json);
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(AppManager));
            manager = (AppManager)deseralizer.ReadObject(ms);
            return manager;
        }

        public string GetConfig(string AppUID)
        {
            string path = ConfigManager.GetInstance().SystemConfigDirectory + AppUID + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string config = path + AppUID;
            if (!File.Exists(config))
            {
                FileStream stream = File.Create(config);
                stream.Close();
            }

            return config;
        }

        public void SelectApp(string AppUID)
        {
            SelectAppNotify(AppUID);
        }

        public void AddApp(AppItem newItem)
        {
            for(int page = 0; page <= SDSystem.AppMaxPageCount; page++)
            {
                for(int row = 1; row <= SDSystem.AppMaxRowsCount; row++)
                {
                    for(int column = 1; column <= SDSystem.AppMaxColumnsCount; column++)
                    {
                        bool isExist = false;
                        foreach(AppItem item in AppItemList)
                        {
                            if (item.Page == page && item.ColumnID == column && item.RowID == row)
                            {
                                isExist = true;
                            }
                        }

                        if (!isExist)
                        {
                            newItem.Page = page;
                            newItem.RowID = row;
                            newItem.ColumnID = column;
                            AppItemList.Add(newItem);
                            SaveApp();
                            return;
                        }
                    }
                }
            }
        }

        public void UpdateApp(AppItem item)
        {
            for(int i = 0; i < AppItemList.Count; i++)
            {
                if(AppItemList.ElementAt(i).AppID == item.AppID)
                {
                    AppItemList.RemoveAt(i);
                    AppItemList.Add(item);
                    SaveApp();
                    break;
                }
            }
        }

        public void DeleteApp(AppItem item)
        {
            int page = item.Page;
            AppItemList.Remove(item);

            bool isExist = false;
            foreach(AppItem i in AppItemList)
            {
                if(i.Page == page)
                {
                    isExist = true;
                    break;
                }
            }

            if(!isExist)
            {
                foreach (AppItem i in AppItemList)
                {
                    if (i.Page > page)
                    {
                        i.Page--;
                    }
                }
            }

            SaveApp();
        }

        public void ClearApp()
        {
            AppItemList.Clear();

            SaveApp();
        }

        private void SaveApp()
        {
            PageList.Clear();
            foreach(AppItem item in AppItemList)
            {
                if (!PageList.ContainsKey(item.Page))
                {
                    PageList.Add(item.Page, item.Page);
                }
            }

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(AppManager));
            MemoryStream memoryStream = new MemoryStream();
            js.WriteObject(memoryStream, AppManager.GetInstance());
            memoryStream.Position = 0;
            StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
            string json = streamReader.ReadToEnd();
            streamReader.Close();
            memoryStream.Close();
            json = MD5.Encrypt(json);
            File.WriteAllText(ConfigPath, json);

            Update();
        }
    }
}
