using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SecondDesktopDesktopManagerDll
{
    [DataContract]
    public class DesktopPageItem
    {
        [DataMember]
        public int Page { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public bool IsSelect
        {
            get
            {
                return isSelect;
            }
            set
            {
                if (value)
                {
                    Image = focusImage;
                }
                else
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
