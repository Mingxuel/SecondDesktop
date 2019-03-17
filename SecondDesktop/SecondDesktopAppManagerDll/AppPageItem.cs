using SecondDesktopDll;
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
        public string Image
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
        private string image = null;

        private readonly string focusImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/AppPageFocus.png";
        private readonly string unFocusImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/AppPageUnFocus.png";
    }
}
