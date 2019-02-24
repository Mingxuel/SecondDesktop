using SecondDesktopDll;

namespace SecondDesktop
{
    class VMAppPanelFrame : NotifyPropertyChanged
    {
        public MAppPanelFrame Model { get; set; }
        public VMAppPanelFrame()
        {
            Model = new MAppPanelFrame();
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
