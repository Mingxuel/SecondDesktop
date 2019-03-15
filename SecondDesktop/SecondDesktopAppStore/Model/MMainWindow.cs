using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktopAppStore
{
	class MMainWindow
	{
        public MMainWindow()
        {
            AppItems = new ObservableCollection<AppStoreItem>();
        }

        public ObservableCollection<AppStoreItem> AppItems;
    }
}
