using SecondDesktopDll;
using System;
using System.Windows.Media.Imaging;

namespace SecondDesktop
{
    class VMAppPanelFrame : NotifyObject
    {
		public delegate void CloseDelegate();
		public event CloseDelegate CloseNotify;

		public MAppPanelFrame Model { get; set; }
        public VMAppPanelFrame()
        {
            Model = new MAppPanelFrame();
			CloseImage = SDResource.DeleteImage;
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

		private SDCommand<string> closeClickCommand;
		public SDCommand<string> CloseClickCommand
		{
			get
			{
				if (closeClickCommand == null)
					closeClickCommand = new SDCommand<string>(
						new Action<string>(e =>
						{
							CloseNotify();
						}), null);
				return closeClickCommand;
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

		public string CloseImage
		{
			get { return Model.CloseImage; }
			set
			{
				Model.CloseImage = value;
				RaisePropertyChanged("CloseImage");
			}
		}
	}
}
