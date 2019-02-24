using SecondDesktopDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktop
{
    class VMSubAppFrame : NotifyPropertyChanged
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
                this.SetAndNotify(ref Model.Width, value, () => Model.Width);
            }
        }

        public double Height
        {
            get { return Model.Height; }
            set
            {
                this.SetAndNotify(ref Model.Height, value, () => Model.Height);
            }
        }
    }
}
