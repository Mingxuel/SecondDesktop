using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktopDesktopManagerDll
{
    class DesktopWindowManager
    {
        private static DesktopWindowManager windowManager = null;
        public static DesktopWindowManager GetInstance()
        {
            if (windowManager == null)
            {
                windowManager = new DesktopWindowManager();
            }

            return windowManager;
        }
    }
}
