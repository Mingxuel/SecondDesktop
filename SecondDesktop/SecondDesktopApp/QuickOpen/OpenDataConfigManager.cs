using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace QuickOpen
{
    public class OpenData
    {
        private List<OpenDataItem> openDataList = null;
        public List<OpenDataItem> OpenDataList
        {
            get
            {
                if (openDataList == null)
                {
                    openDataList = new List<OpenDataItem>();
                }
                return openDataList;
            }
            set
            {
                openDataList = value;
            }
        }
    }

	/// <summary>
	/// class name: SubAppConfigManager
	/// Congig manager
	/// </summary>
	public class OpenDataConfigManager
    {
        private OpenData openData = null;
        private string ConfigPath = "";
        public OpenDataConfigManager(string pConfig)
        {
            Init(pConfig);
        }

        private void Init(string pConfig)
        {
            ConfigPath = pConfig;
            if (!File.Exists(ConfigPath))
            {
                FileStream stream = File.Create(ConfigPath);
                stream.Close();

                File.WriteAllText(ConfigPath, "[]");
            }

            string json = File.ReadAllText(ConfigPath);
            if(json == "")
            {
                json = "[]";
            }

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(OpenData));
            openData = (OpenData)deseralizer.ReadObject(ms);
        }

        public int Count()
        {
            return openData.OpenDataList.Count();
        }

        public OpenData GetData()
        {
            return openData;
        }

        public void CreateData(int count)
        {
            openData.OpenDataList.Clear();
            for(int i = 0; i < count; i++)
            {
                OpenDataItem item = new OpenDataItem();
                item.UID = i;
                openData.OpenDataList.Add(item);
            }
            SaveData();
        }

        public void SaveData()
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(OpenData));
            MemoryStream memoryStream = new MemoryStream();
            js.WriteObject(memoryStream, openData);
            memoryStream.Position = 0;
            StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
            string json = streamReader.ReadToEnd();
            streamReader.Close();
            memoryStream.Close();
            File.WriteAllText(ConfigPath, json);
        }
    }
}
