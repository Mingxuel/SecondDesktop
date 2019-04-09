using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktopMessagerDll
{
    public class SecondDesktopMessager
    {
        public delegate void CreateSubAppDelegate(string AppUID, int WindowUID, string Config);
        public event CreateSubAppDelegate CreateSubAppNotify;

        public delegate string CreateSubAppConfigDelegate(string AppUID);
        public event CreateSubAppConfigDelegate CreateSubAppConfigNotify;

        public delegate void DesktopSettingsDelegate(bool IsSetting);
        public event DesktopSettingsDelegate DesktopSettingsNotify;

        public delegate void ShowMessageBoxDelegate(string pMessage, Func<bool, int> pReturn);
        public event ShowMessageBoxDelegate ShowMessageBoxNotify;

        private static SecondDesktopMessager secondDesktopMessager = null;
        public static SecondDesktopMessager GetInstance()
        {
            if (secondDesktopMessager == null)
            {
                secondDesktopMessager = new SecondDesktopMessager();
            }

            return secondDesktopMessager;
        }

        public void CreateSubApp(string AppUID, int WindowUID, string Config)
        {
            CreateSubAppNotify(AppUID, WindowUID, Config);
        }

        public string CreateSubAppConfig(string AppUID)
        {
            return CreateSubAppConfigNotify(AppUID);
        }

        public void DesktopSettings(bool IsSetting)
        {
            if(DesktopSettingsNotify != null)
            {
                DesktopSettingsNotify(IsSetting);
            }
        }

        public void ShowMessageBox(string pMessage, Func<bool, int> Return)
        {
            if (ShowMessageBoxNotify != null)
            {
                ShowMessageBoxNotify(pMessage, Return);
            }
        }
    }
}
