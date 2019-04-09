using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickOpen
{
    class VMOpenWindow : NotifyObject
    {
        private string Config = null;
        private MOpenWindow Model = null;
        public VMOpenWindow(string pConfig)
        {
            Model = new MOpenWindow();
            Config = pConfig;
            update();
        }

        public ObservableCollection<OpenDataItem> OpenDataItems
        {
            get
            {
                return Model.OpenDataItems;
            }
            set
            {
                Model.OpenDataItems = value;
                RaisePropertyChanged("OpenDataItems");
            }
        }

        private void update()
        {
            OpenDataConfigManager manager = new OpenDataConfigManager(Config);
            OpenData data = manager.GetData();
            foreach(var item in data.OpenDataList)
            {
                OpenDataItems.Add(item);
            }
        }
    }
}
