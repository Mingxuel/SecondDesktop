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

        public Visibility AppMainWindowVisibility;
        public string AppIcon;
        public string AppTitle;
        public string AppCloseImage;
    }
}