﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktopDll
{
    public class ConfigManager
    {
        private static ConfigManager manager = null;
        public static ConfigManager GetInstance()
        {
            if(manager == null)
            {
                manager = new ConfigManager();
                manager.Init();
            }

            return manager;
        }

        private void Init()
        {
            if(!Directory.Exists(SystemConfigDirectory))
            {
                Directory.CreateDirectory(SystemConfigDirectory);
            }

            if (!Directory.Exists(ApplicationConfigDirectory))
            {
                Directory.CreateDirectory(ApplicationConfigDirectory);
            }
        }

        private string ConfigPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SecondDesktop";
            }
        }

        public string SystemConfigDirectory
        {
            get
            {
                return ConfigPath + "\\SysConfig\\";
            }
        }

        public string ApplicationConfigDirectory
        {
            get
            {
                return ConfigPath + "\\AppConfig\\";
            }
        }

		public string ApplicationAppsDirectory
		{
			get
			{
				return ConfigPath + "\\Apps\\";
			}
		}

		public string ApplicationDirectory
		{
			get
			{
				return System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			}
		}

        public string ApplicationStoreDirectory
        {
            get
            {
                return ApplicationDirectory + "Apps\\";
            }
        }
    }
}
