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
    public class DesktopManager
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

        private static string ConfigPath = "";
        private static DesktopManager desktopManager = null;
        public static DesktopManager GetInstance()
        {
            if (desktopManager == null)
            {
                desktopManager = Init();
            }

            return desktopManager;
        }

        private static DesktopManager Init()
        {
            DesktopManager manager = null;
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
            DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(DesktopManager));
            manager = (DesktopManager)deseralizer.ReadObject(ms);

            SecondDesktopMessager.GetInstance().CreateSubAppConfigNotify += manager.CreateSubAppConfig;

            return manager;
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
            SaveSubApp();
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
                foreach (var i in PageList)
                {
                    if(i.Key >= page)
                    {
                        string title = i.Value;
                        PageList.Remove(i.Key);
                        PageList.Add(page - 1, title);
                    }
                }
            }

            SaveSubApp();
        }

        public void AddPage(int Page, string Title)
        {
            PageList.Add(Page, Title);
            SaveSubApp();
        }

        public void ModifyPage(int Page, string Title)
        {
            PageList[Page] = Title;
            SaveSubApp();
        }

        private void SaveSubApp()
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(DesktopManager));
            MemoryStream memoryStream = new MemoryStream();
            js.WriteObject(memoryStream, DesktopManager.GetInstance());
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
    }
}
