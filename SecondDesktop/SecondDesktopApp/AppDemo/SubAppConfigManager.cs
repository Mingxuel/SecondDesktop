using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace AppDemo
{
	/// <summary>
	/// class name: SubAppConfigManager
	/// Congig manager
	/// </summary>
	public class SubAppConfigManager
    {
        private string ConfigPath = "";
        private SubAppData data = new SubAppData();
        public SubAppConfigManager(string pConfig)
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
            DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(SubAppData));
            data = (SubAppData)deseralizer.ReadObject(ms);
        }

        public string GetText()
        {
            return data.TestText;
        }

        public void SetText(string pText)
        {
            data.TestText = pText;
            SaveApp();
        }

        public void SaveApp()
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SubAppData));
            MemoryStream memoryStream = new MemoryStream();
            js.WriteObject(memoryStream, data);
            memoryStream.Position = 0;
            StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
            string json = streamReader.ReadToEnd();
            streamReader.Close();
            memoryStream.Close();
            File.WriteAllText(ConfigPath, json);
        }
    }
}
