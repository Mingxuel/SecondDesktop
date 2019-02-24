using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktopDesktopManagerDll
{
    [DataContract]
    public class DesktopItem
    {
        [DataMember]
        public int Page { get; set; }
        [DataMember]
        public string AppUID { get; set; }
        [DataMember]
        public int SubAppUID { get; set; }
        [DataMember]
        public string Config { get; set; }
    }
}
