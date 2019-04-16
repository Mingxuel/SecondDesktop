using MaterialDesignThemes.Wpf;
using SecondDesktopDll;
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
