using MaterialDesignThemes.Wpf;
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
                    Image = PackIconKind.CheckboxBlankCircleOutline;
                }
                else
                {
                    Image = PackIconKind.CheckboxBlankCircle;
                }
                isSelect = value;
            }
        }
        private bool isSelect = false;
        [DataMember]
        public PackIconKind Image
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
        private PackIconKind image = PackIconKind.StarFourPoints;
    }
}
