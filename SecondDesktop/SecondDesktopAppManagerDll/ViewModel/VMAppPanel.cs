using SecondDesktopDll;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktopAppManagerDll
{
    class VMAppPanel : NotifyObject
    {
        private MAppPanel Model = null;
        private const double HideHeight = 20;
        private const double ShowHeight = 175;

        public VMAppPanel()
        {
            Model = new MAppPanel();
            Height = ShowHeight;
            Icon = "▽";
        }

        private SDCommand<string> appClickCommand;
        public SDCommand<string> AppClickCommand
        {
            get
            {
                if (appClickCommand == null)
                    appClickCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            if(Height != HideHeight)
                            {
                                Height = HideHeight;
                                Icon = "△";
                            } else
                            {
                                Height = ShowHeight;
                                Icon = "▽";
                            }
                        }), null);
                return appClickCommand;
            }
        }

        public double Height
        {
            get { return Model.Height; }
            set
            {
                Model.Height = value;
                RaisePropertyChanged("Height");
            }
        }

        public string Icon
        {
            get { return Model.Icon; }
            set
            {
                Model.Icon = value;
                RaisePropertyChanged("Icon");
            }
        }
    }
}
