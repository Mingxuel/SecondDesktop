using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace SecondDesktopHotkeyManagerDll
{
    public class HotkeyManager
    {
        List<HotKey> HotKeyList = new List<HotKey>();

        private static HotkeyManager hotkeyManager = null;
        public static HotkeyManager GetInstance()
        {
            if(hotkeyManager == null)
            {
                hotkeyManager = new HotkeyManager();
            }

                return hotkeyManager;
        }

        public void StartListen()
        {
            keyEventHandeler = new KeyEventHandler(hook_KeyDown);
            k_hook.KeyDownEvent += keyEventHandeler;
            k_hook.Start();
        }

        public void StopListen()
        {
            if (keyEventHandeler != null)
            {
                k_hook.KeyDownEvent -= keyEventHandeler;
                keyEventHandeler = null;
                k_hook.Stop();
            }
        }
        
        public bool RegisterHotKey(string pName, LKey pLKey, RKey pRKey, Func<int> pDelegate)
        {
            foreach(var item in HotKeyList)
            {
                if(item.Name == pName || (item.LKey == pLKey && item.RKey == pRKey))
                {
                    return false;
                }
            }

            HotKeyList.Add(new HotKey(pName, pLKey, pRKey, pDelegate));

            return true;
        }

        private KeyEventHandler keyEventHandeler = null;
        private KeyboardHook k_hook = new KeyboardHook();

        private void hook_KeyDown(object sender, KeyEventArgs e)
        {
			foreach (var item in HotKeyList)
            {
                if (((int)item.LKey == (int)Control.ModifierKeys) && ((int)item.RKey == (int)e.KeyCode))
                {
                    item.Delegate();
                    break;
                }
            }
        }
    }
}
