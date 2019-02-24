using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;

namespace SecondDesktopAppManagerDll
{
    [DataContract]
    public class AppPageItem
    {
        [DataMember]
        public int Page { get; set; }
        [DataMember]
        public bool IsSelect {
            get
            {
                return isSelect;
            }
            set
            {
                if(value)
                {
                    Image = focusImage;
                } else
                {
                    Image = unFocusImage;
                }
                isSelect = value;
            }
        }
        private bool isSelect = false;
        [DataMember]
        public BitmapImage Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }
        private BitmapImage image = null;

        private readonly BitmapImage focusImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/AppPageFocus.png", UriKind.Absolute));
        private readonly BitmapImage unFocusImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/AppPageUnFocus.png", UriKind.Absolute));
    }
}
