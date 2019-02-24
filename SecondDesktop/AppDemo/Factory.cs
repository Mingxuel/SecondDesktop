using SecondDesktopMessagerDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AppDemo
{
    enum AppUID : int
    {
        UCDemo = 0,//Please keep your MainWindow UID is zero.
        SubApp = 1,
    }

    class Factory
    {
        private static string AppName = "AppDemo";
        public Factory(){
            
        }
        
        public UserControl CreateWindow(int WindowUID, string Config)
        {
            switch(WindowUID)
            {
                case (int)AppUID.UCDemo:
                    return new UCDemo(Config);
                case (int)AppUID.SubApp:
                    return new SubApp(Config);
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
