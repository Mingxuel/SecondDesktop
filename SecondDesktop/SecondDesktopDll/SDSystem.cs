using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SecondDesktopDll
{
    public class SDSystem
    {
        public static int WindowWidth = 348;
        public static int WindowMargin = 2;
        public static int AppMaxPageCount = 999;
        public static int AppMaxColumnsCount = 7;
        public static int AppMaxRowsCount = 2;
        public static int DesktopMaxPageCount = 999;
        public static double DesktopHeight
        {
            get
            {
                return SystemParameters.WorkArea.Size.Height - 58.0;
            }
        }
    }
}
