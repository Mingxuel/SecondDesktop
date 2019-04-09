using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickOpen
{
    class MOpenWindow
    {
        public MOpenWindow()
        {
            OpenDataItems = new ObservableCollection<OpenDataItem>();
        }

        public ObservableCollection<OpenDataItem> OpenDataItems;
    }
}
