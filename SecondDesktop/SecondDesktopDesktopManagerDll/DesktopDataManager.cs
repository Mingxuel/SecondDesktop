using MaterialDesignColors;
using SecondDesktopDll;
using SecondDesktopMessagerDll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktopDesktopManagerDll
{
	public class DesktopDataManager
	{
		public delegate void UpdateDelegate();
		public event UpdateDelegate Update;

		public List<DesktopItem> desktopItemList = null;
		public List<DesktopItem> DesktopItemList
		{
			get
			{
				if (desktopItemList == null)
				{
					desktopItemList = new List<DesktopItem>();
				}
				return desktopItemList;
			}
			set
			{
				desktopItemList = value;
			}
		}

		public Dictionary<int, string> pageList = null;
		public Dictionary<int, string> PageList
		{
			get
			{
				if (pageList == null)
				{
					pageList = new Dictionary<int, string>();
				}
				return pageList;
			}
			set
			{
				pageList = value;
			}
		}

		private DesktopTheme theme;
		public DesktopTheme Theme
		{
			get
			{
				if (theme == null)
				{
					theme = new DesktopTheme();
				}
				return theme;
			}
			set
			{
				theme = value;
			}
		}

		public void SetThemeDark(bool IsDark)
		{
			Theme.IsDark = IsDark;
			Save();
		}

		public void SetThemeColor(int Color)
		{
			Theme.Color = Color;
			Save();
		}

		private static string ConfigPath = "";
        private static DesktopDataManager desktopManager = null;
        public static DesktopDataManager GetInstance()
        {
            if (desktopManager == null)
            {
                Init();
            }

            return desktopManager;
        }

        private static void Init()
        {
            ConfigPath = ConfigManager.GetInstance().SystemConfigDirectory + "desktop.config";
            if (!File.Exists(ConfigPath))
            {
                FileStream stream = File.Create(ConfigPath);
                stream.Close();

                File.WriteAllText(ConfigPath, MD5.Encrypt("[]"));
            }

            string json = File.ReadAllText(ConfigPath);
            json = MD5.Decrypt(json);
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(DesktopDataManager));
            desktopManager = (DesktopDataManager)deseralizer.ReadObject(ms);
            SecondDesktopMessager.GetInstance().CreateSubAppConfigNotify += desktopManager.CreateSubAppConfig;

            if(desktopManager.PageList.Count() == 0)
            {
                desktopManager.AddPage(0, "DESKTOP");
            }
		}

        public string CreateSubAppConfig(string AppUID)
        {
            string path = ConfigManager.GetInstance().ApplicationConfigDirectory + AppUID + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string config = path + AppUID + Guid.NewGuid().ToString();
            if (!File.Exists(config))
            {
                FileStream stream = File.Create(config);
                stream.Close();
            }

            return config;
        }

        public void AddSubApp(DesktopItem Item)
        {
            DesktopItemList.Add(Item);
            Save();
        }

        public void DeleteSubApp(DesktopItem Item)
        {
            int page = Item.Page;
            DesktopItemList.Remove(Item);

            if (File.Exists(Item.Config))
            {
                File.Delete(Item.Config);
            }

            bool isExist = false;
            foreach (DesktopItem i in DesktopItemList)
            {
                if (i.Page == page)
                {
                    isExist = true;
                    break;
                }
            }

            if (!isExist)
            {
                foreach (DesktopItem i in DesktopItemList)
                {
                    if (i.Page > page)
                    {
                        i.Page--;
                    }
                }

                PageList.Remove(page);
                Dictionary<int, string> tempList = new Dictionary<int, string>();
                for (int i = 0; i < PageList.Count(); i++)
                {
                    if (PageList.ElementAt(i).Key > page)
                    {
                        tempList.Add(PageList.ElementAt(i).Key - 1, PageList.ElementAt(i).Value);
                        PageList.Remove(PageList.ElementAt(i).Key);
                        i--;
                    }
                }
                foreach (var item in tempList)
                {
                    PageList.Add(item.Key, item.Value);
                }
            }

            Save();
        }

        public void AddPage(int Page, string Title)
        {
            PageList.Add(Page, Title);
            Save();
        }

        public void DeletePage(int Page)
        {
            for(int i = 0; i<DesktopItemList.Count; i++)
            {
                if(DesktopItemList.ElementAt(i).Page == Page)
                {
                    DesktopItemList.RemoveAt(i);
                    i--;
                }
            }

            PageList.Remove(Page);
            Dictionary<int, string> tempList = new Dictionary<int, string>();
            for(int i = 0; i< PageList.Count(); i++)
            {
                if (PageList.ElementAt(i).Key > Page)
                {
                    tempList.Add(PageList.ElementAt(i).Key - 1, PageList.ElementAt(i).Value);
                    PageList.Remove(PageList.ElementAt(i).Key);
                    i--;
                }
            }

            foreach (var item in tempList)
            {
                PageList.Add(item.Key, item.Value);
            }

            PageList = PageList.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);

            Save();
        }

        public void ModifyPage(int Page, string Title)
        {
            PageList[Page] = Title;
            Save();
        }

        public void Save()
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(DesktopDataManager));
            MemoryStream memoryStream = new MemoryStream();
            js.WriteObject(memoryStream, DesktopDataManager.GetInstance());
            memoryStream.Position = 0;
            StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
            string json = streamReader.ReadToEnd();
            streamReader.Close();
            memoryStream.Close();
            json = MD5.Encrypt(json);
            File.WriteAllText(ConfigPath, json);

            if(Update != null)
                Update();
        }

        public void UpdateDesktop()
        {
            if (Update != null)
                Update();
        }
    }
}
