using SecondDesktopDll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public delegate void SettingAppNotifyDelegate(bool IsSetting);
        public event SettingAppNotifyDelegate SettingAppNotify;

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
                Init();
            }

            return appManager;
        }

        private static void Init()
        {
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
			appManager = (AppManager)deseralizer.ReadObject(ms);

			appManager.InitApps();
        }

		private void InitApps()
		{
            Dictionary<string, Version> storeAppList = new Dictionary<string, Version>();
            string storeDirectory = ConfigManager.GetInstance().ApplicationStoreDirectory;
            if (!Directory.Exists(storeDirectory))
            {
                Directory.CreateDirectory(storeDirectory);
            }
            string[] tempList = Directory.GetFileSystemEntries(storeDirectory);
            for(int i = 0; i < tempList.Count(); i++)
            {
                string app = tempList[i].Substring(tempList[i].LastIndexOf("\\")+1);
                Version version = AssemblyName.GetAssemblyName(tempList[i]+ "\\" + app +".dll").Version;
                storeAppList.Add(app, version);
            }

            Dictionary<string, Version> dataAppList = new Dictionary<string, Version>();
            string dataDirectory = ConfigManager.GetInstance().ApplicationAppsDirectory;
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
            tempList = Directory.GetFileSystemEntries(dataDirectory);
            for (int i = 0; i < tempList.Count(); i++)
            {
                string app = tempList[i].Substring(tempList[i].LastIndexOf("\\") + 1);
                Version version = AssemblyName.GetAssemblyName(tempList[i] + "\\" + app + ".dll").Version;
                dataAppList.Add(app, version);
            }

            foreach(var storeApp in storeAppList)
            {
                bool cover = true;
                foreach(var dataApp in dataAppList)
                {
                    if(storeApp.Key == dataApp.Key && storeApp.Value <= dataApp.Value)
                    {
                        cover = false;
                        break;
                    }
                }

                if (cover)
                {
                    if(Directory.Exists(dataDirectory + storeApp.Key))
                    {
                        Directory.Delete(dataDirectory + storeApp.Key, true);
                    }
                    CopyDirectory(storeDirectory + storeApp.Key, dataDirectory + storeApp.Key);
                }
            }

            //Add appstore
            if (!ExistsApp("SecondDesktopAppStore"))
			{
				string path = ConfigManager.GetInstance().ApplicationDirectory + "SecondDesktopAppStore";
				AddApp(path);
			}
		}

		public static void CopyDirectory(string pOldPath, string pNewPath)
		{
			try
			{
				if (pNewPath[pNewPath.Length - 1] != Path.DirectorySeparatorChar)
					pNewPath += Path.DirectorySeparatorChar;
				if (!Directory.Exists(pNewPath))
					Directory.CreateDirectory(pNewPath);
				string[] fileList = Directory.GetFileSystemEntries(pOldPath);
				foreach (string file in fileList)
				{
					if (Directory.Exists(file))
						CopyDirectory(file, pNewPath + Path.GetFileName(file));
					// 否则直接Copy文件
					else
						File.Copy(file, pNewPath + Path.GetFileName(file), true);
				}
			}
			catch
			{
				Console.WriteLine("无法复制!");
			}
		}

		public string GetConfig(string AppUID)
        {
            string path = ConfigManager.GetInstance().ApplicationConfigDirectory + AppUID + "\\";
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

        public void SettingApp(bool IsSetting)
        {
            SettingAppNotify(IsSetting);
        }

        public bool ExistsApp(string pAppUID)
		{
			foreach (AppItem item in AppItemList)
			{
				if (item.AppUID == pAppUID)
				{
					return true;
				}
			}
			return false;
		}

        public AppItem LoadApp(string pPath)
        {
            AppItem item = new AppItem();

            string appUID = pPath.Substring(pPath.LastIndexOf("\\") + 1);
            item.AppUID = appUID;
            string newPath = ConfigManager.GetInstance().ApplicationAppsDirectory + appUID;
            CopyDirectory(pPath, newPath);
            string[] fileList = Directory.GetFileSystemEntries(newPath);
            foreach (string file in fileList)
            {
                if (file.Contains(".png"))
                {
                    item.Icon = file;
                    item.Name = file.Substring(file.LastIndexOf("\\") + 1).Replace(".png", "");
                    break;
                }
            }

            return item;
        }

        public void AddApp(string pPath, AppStatus pStatus = AppStatus.Normal)
		{
			AppItem item = new AppItem();

			string appUID = pPath.Substring(pPath.LastIndexOf("\\") + 1);
			item.AppUID = appUID;
			string newPath = ConfigManager.GetInstance().ApplicationAppsDirectory + appUID;
			CopyDirectory(pPath, newPath);
			string[] fileList = Directory.GetFileSystemEntries(newPath);
			foreach (string file in fileList)
			{
				if (file.Contains(".png"))
				{
					item.Icon = file;
					item.Name = file.Substring(file.LastIndexOf("\\") + 1).Replace(".png", "");
					break;
				}
			}
			item.Config = GetConfig(appUID);
			item.Status = pStatus;
			AddApp(item);
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
                if(AppItemList.ElementAt(i).AppUID == item.AppUID)
                {
                    AppItemList.RemoveAt(i);
                    AppItemList.Add(item);
                    SaveApp();
                    break;
                }
            }
        }

        public void DeleteApp(string pAppUID)
        {
            foreach (AppItem item in AppItemList)
            {
                if (item.AppUID == pAppUID)
                {
                    DeleteApp(item);
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

        public void SaveApp()
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
            js.WriteObject(memoryStream, appManager);
            memoryStream.Position = 0;
            StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
            string json = streamReader.ReadToEnd();
            streamReader.Close();
            memoryStream.Close();
            json = MD5.Encrypt(json);
            File.WriteAllText(ConfigPath, json);

			if(Update != null)
			{
				Update();
			}
        }
    }
}
