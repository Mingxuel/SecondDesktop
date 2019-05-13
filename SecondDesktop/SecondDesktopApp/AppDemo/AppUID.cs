using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDemo
{

    /// <summary>
    /// Description: Window UID, please make your SubApp name and AppUID has the same name.
    /// </summary>
    enum AppUID : int
    {
        MainWindow = 0,//Please keep the MainWindow at 0.
        AppWindow = 1,//SubApp UID
                      //AppWindow1 = 2,
    }

}
