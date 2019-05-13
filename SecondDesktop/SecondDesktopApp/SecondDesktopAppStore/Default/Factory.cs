using SecondDesktopMessagerDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SecondDesktopAppStore
{
    class Factory
    {
        private static string AppName = "SecondDesktopAppStore";
        public Factory(){
            
        }
        
        public UserControl CreateWindow(int WindowUID, string Config)
        {
            switch(WindowUID)
            {
                case (int)AppUID.MainWindow:
                    return new MainWindow(Config);
                default:
                    break;
            }

            return null;
        }

        public static string CreateSubAppConfig()
        {
            return SecondDesktopMessager.GetInstance().CreateSubAppConfig(Factory.AppName);
        }

        public static void CreateSubApp(AppUID UID, string Config)
        {
            SecondDesktopMessager.GetInstance().CreateSubApp(Factory.AppName, (int)UID, Config);
        }
    }
}
