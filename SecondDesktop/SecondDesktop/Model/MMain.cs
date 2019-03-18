using SecondDesktopDesktopManagerDll;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SecondDesktop
{
    class MMain
    {
        public MMain()
        {
            DesktopItems = new ObservableCollection<DesktopItem>();
            DesktopPageItems = new ObservableCollection<DesktopPageItem>();
		}

        public double Width;
        public double Height;
        public double Left;
        public double Top;
        public double AppHeight;
        public double PageHeight;
        public Visibility AppMainWindowVisibility;
        public int CurrentPage;

        public string DesktopTitle;
        public string DesktopTitleBackground;
        public bool DesktopTitleReadOnly;
        public string DesktopEdit;

        public ObservableCollection<DesktopItem> DesktopItems;
        public ObservableCollection<DesktopPageItem> DesktopPageItems;

		public string DesktopImage;
		public string DesktopSettingImage;
		public string DesktopAddImage;
        public string DesktopDeleteImage;

        public string AppIcon;
        public string AppTitle;
        public string AppCloseImage;
    }
}