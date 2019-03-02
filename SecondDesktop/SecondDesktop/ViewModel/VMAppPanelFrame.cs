using SecondDesktopDll;

namespace SecondDesktop
{
    class VMAppPanelFrame : NotifyObject
    {
        public MAppPanelFrame Model { get; set; }
        public VMAppPanelFrame()
        {
            Model = new MAppPanelFrame();
        }
        public string Title
        {
            get { return Model.Title; }
            set
            {
                Model.Title = value;
                RaisePropertyChanged("Title");
            }
        }
    }
}
