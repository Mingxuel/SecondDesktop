using SecondDesktopDll;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private SDCommand<string> settingsClickCommand;
        public SDCommand<string> SettingsClickCommand
        {
            get
            {
                if (settingsClickCommand == null)
					settingsClickCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {

                        }), null);
                return settingsClickCommand;
            }
        }

		private SDCommand<string> foldClickCommand;
		public SDCommand<string> FoldClickCommand
		{
			get
			{
				if (foldClickCommand == null)
					foldClickCommand = new SDCommand<string>(
						new Action<string>(e =>
						{
							if (Height != HideHeight)
							{
								Height = HideHeight;
								AppsFoldImage = SDResource.UpImage;
							}
							else
							{
								Height = ShowHeight;
								AppsFoldImage = SDResource.DownImage;
							}
						}), null);
				return foldClickCommand;
			}
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

		public BitmapImage AppsImage
		{
			get { return Model.AppsImage; }
			set
			{
				Model.AppsImage = value;
				RaisePropertyChanged("AppsImage");
			}
		}

		public BitmapImage AppsSettingImage
		{
			get { return Model.AppsSettingImage; }
			set
			{
				Model.AppsSettingImage = value;
				RaisePropertyChanged("AppsSettingImage");
			}
		}

		public BitmapImage AppsFoldImage
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
