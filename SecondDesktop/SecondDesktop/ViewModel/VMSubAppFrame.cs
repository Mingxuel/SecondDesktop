using SecondDesktopDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktop
{
    class VMSubAppFrame : NotifyObject
	{
        public MSubAppFrame Model { get; set; }
        public VMSubAppFrame()
        {
            Model = new MSubAppFrame();
        }
        public double Width
        {
            get { return Model.Width; }
            set
            {
				Model.Width = value;
				RaisePropertyChanged("Width");
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
    }
}
