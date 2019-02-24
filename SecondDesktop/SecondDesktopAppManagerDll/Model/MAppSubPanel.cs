using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktopAppManagerDll
{
    class MAppSubPanel
    {
        public MAppSubPanel()
        {
            AppItems = new ObservableCollection<AppItem>();
            PageItems = new ObservableCollection<AppPageItem>();
        }
        public ObservableCollection<AppItem> AppItems;
        public ObservableCollection<AppPageItem> PageItems;
    }
}
