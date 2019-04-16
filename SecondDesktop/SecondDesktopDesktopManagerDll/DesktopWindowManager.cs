using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktopDesktopManagerDll
{
    public class DesktopWindowManager
    {
        public delegate void DesktopSettingsDelegate(bool pIsSettings);
        public event DesktopSettingsDelegate DesktopSettingsCallback;

        private static DesktopWindowManager windowManager = null;
        public static DesktopWindowManager GetInstance()
        {
            if (windowManager == null)
            {
                windowManager = new DesktopWindowManager();
            }

            return windowManager;
        }

		private int currentPage = 0;
		public int CurrentPage
		{
			get
			{
				return currentPage;
			}
			set
			{
				currentPage = value;
			}
		}

		private bool isSettings = false;
		public bool IsSettings
		{
			get
			{
				return isSettings;
			}
			set
			{
				isSettings = value;
			}
		}

		public void SettingsDesktop()
		{
			IsSettings = !IsSettings;
            if (DesktopSettingsCallback != null)
                DesktopSettingsCallback(IsSettings);
        }

		public void AddDesktop()
		{
			int page = 0;
			foreach (var item in DesktopDataManager.GetInstance().PageList)
			{
				if (page <= item.Key)
				{
					page = item.Key + 1;
				}
			}

			DesktopDataManager.GetInstance().AddPage(page, "DESKTOP");
			CurrentPage = page;
            UpdateDesktop();
        }

		public void DeleteDesktop()
		{
			DesktopDataManager.GetInstance().DeletePage(CurrentPage);
            CurrentPage = 0;
            UpdateDesktop();
        }

        public void UpdateDesktop()
        {
            DesktopDataManager.GetInstance().UpdateDesktop();
        }
    }
}
