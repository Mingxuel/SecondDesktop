using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktopHotkeyManagerDll
{
    class HotKey
    {
        public HotKey(string pName, LKey pLKey, RKey pRKey, Func<int> pDelegate)
        {
            Name = pName;
			LKey = pLKey;
			RKey = pRKey;
            Delegate = pDelegate;
        }

        public string		Name;
        public LKey			LKey;
        public RKey			RKey;
        public Func<int>	Delegate;
    }
}
