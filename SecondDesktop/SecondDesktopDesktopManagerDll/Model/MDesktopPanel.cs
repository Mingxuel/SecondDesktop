using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SecondDesktopDesktopManagerDll
{
    class MDesktopPanel
    {
        public MDesktopPanel()
        {
            DesktopItems = new ObservableCollection<DesktopItem>();
            DesktopPageItems = new ObservableCollection<DesktopPageItem>();
        }

        public double Width;
        public double Height;
        public double Left;
        public double Top;
        public int CurrentPage;

        public string DesktopTitle;
        public string DesktopTitleBackground;
        public bool DesktopTitleReadOnly;

        public ObservableCollection<DesktopItem> DesktopItems;
        public ObservableCollection<DesktopPageItem> DesktopPageItems;

        public string DesktopImage;
        public string DesktopSettingImage;
        public string DesktopAddImage;
        public string DesktopDeleteImage;
    }
}
