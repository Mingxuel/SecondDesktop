using SecondDesktopMessagerDll;
using System;
using System.Reflection;
using System.Windows.Controls;

namespace AppDemo
{
    class Factory
    {
		/// <summary>
		/// Description: DllName
		/// </summary>
        public Factory(){
            
        }

		/// <summary>
		/// Description: Create window, please insert in format if you has subApps.
		/// </summary>
		public UserControl CreateWindow(int WindowUID, string Config)
        {
            string appName = Assembly.GetExecutingAssembly().GetName().Name;
            string dllName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SecondDesktop\\Apps\\" + appName + "\\" + appName + ".dll";
            Assembly assembly = Assembly.LoadFrom(dllName);
            Type type = assembly.GetType(appName + "." + ((AppUID)WindowUID).ToString());
            return (UserControl)System.Activator.CreateInstance(type, Config);
        }

		/// <summary>
		/// Description: Create config file, file name is random.
		/// Path: %(APPDATA)\SecondDesktop\AppConfig\$(TargetName)
		/// Do no edit.
		/// </summary>
		public static string CreateSubAppConfig()
        {
            return SecondDesktopMessager.GetInstance().CreateSubAppConfig(Assembly.GetExecutingAssembly().GetName().Name);
        }

		/// <summary>
		/// Description: To create subApp.
		/// Do no edit.
		/// </summary>
		public static void CreateSubApp(AppUID UID, string Config)
        {
            SecondDesktopMessager.GetInstance().CreateSubApp(Assembly.GetExecutingAssembly().GetName().Name, (int)UID, Config);
        }
    }
}
