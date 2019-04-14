using SecondDesktopDll;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SecondDesktopAppManagerDll
{
    class VMAppPanel : NotifyObject
    {
        private MAppPanel Model = null;
        private const double HideHeight = 20;
        private const double ShowHeight = 175;

        public VMAppPanel()
        {
            Model = new MAppPanel();
            Height = ShowHeight;
			AppsImage = SDResource.AppsImage;
			AppsSettingImage = SDResource.SettingsImage;
			AppsFoldImage = SDResource.DownImage;
        }

		public double Height
        {
            get { return Model.Height; }
            set
            {
                Model.Height = value;
                RaisePropertyChanged("Height");
            }
        }

		public string AppsImage
		{
			get { return Model.AppsImage; }
			set
			{
				Model.AppsImage = value;
				RaisePropertyChanged("AppsImage");
			}
		}

		public string AppsSettingImage
		{
			get { return Model.AppsSettingImage; }
			set
			{
				Model.AppsSettingImage = value;
				RaisePropertyChanged("AppsSettingImage");
			}
		}

		public string AppsFoldImage
		{
			get { return Model.AppsFoldImage; }
			set
			{
				Model.AppsFoldImage = value;
				RaisePropertyChanged("AppsFoldImage");
			}
		}
	}
}
