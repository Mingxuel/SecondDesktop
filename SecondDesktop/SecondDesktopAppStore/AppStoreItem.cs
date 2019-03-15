using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SecondDesktopAppStore
{
    [DataContract]
    class AppStoreItem
    {
        [DataMember]
        public string Icon { get; set; }
        [DataMember]
        public string AppUID { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Status{ get; set; }
        [DataMember]
        public bool IsInstall { get; set; }
    }
}
