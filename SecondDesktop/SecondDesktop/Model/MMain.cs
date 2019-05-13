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

		}

        //MainWindow size
        public double MainWindowWidth;
        public double MainWindowHeight;
        public double MainWindowLeft;

        public Visibility AppWindowVisibility;
        public string AppIcon;
        public string AppTitle;
        public string AppCloseImage;

        public Visibility MessageBoxVisibility;
        public string MessageBoxMessage;

		public Visibility ThemeVisibility;
		public Visibility AppsVisibility;
		public double BottomDrawerHeight;

        public bool IsThemeDark;
    }
}