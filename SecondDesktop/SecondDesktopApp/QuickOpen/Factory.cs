using SecondDesktopMessagerDll;
using System.Windows.Controls;

namespace QuickOpen
{
	/// <summary>
	/// Description: Window UID, please make your SubApp name and AppUID has the same name.
	/// </summary>
	enum AppUID : int
    {
        MainWindow = 0,//Please keep the MainWindow at 0.
        OpenWindow = 1,//SubApp UID
		//AppWindow1 = 2,
    }

    class Factory
    {
		/// <summary>
		/// Description: DllName
		/// </summary>
		private static string AppName = "QuickOpen";
        public Factory(){
            
        }

		/// <summary>
		/// Description: Create window, please insert in format if you has subApps.
		/// </summary>
		public UserControl CreateWindow(int WindowUID, string Config)
        {
            switch(WindowUID)
            {
                case (int)AppUID.MainWindow:
                    return new MainWindow(Config);
                case (int)AppUID.OpenWindow:
                    return new OpenWindow(Config);
				//case (int)AppUID.AppWindow1:
				//	return new AppWindow1(Config);
				default:
                    break;
            }

            return null;
        }

		/// <summary>
		/// Description: Create config file, file name is random.
		/// Path: %(APPDATA)\SecondDesktop\AppConfig\$(TargetName)
		/// Do no edit.
		/// </summary>
		public static string CreateSubAppConfig()
        {
            return SecondDesktopMessager.GetInstance().CreateSubAppConfig(Factory.AppName);
        }

		/// <summary>
		/// Description: To create subApp.
		/// Do no edit.
		/// </summary>
		public static void CreateSubApp(AppUID UID, string Config)
        {
            SecondDesktopMessager.GetInstance().CreateSubApp(Factory.AppName, (int)UID, Config);
        }
    }
}
