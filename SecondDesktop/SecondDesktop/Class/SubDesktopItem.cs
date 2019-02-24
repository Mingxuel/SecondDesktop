using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktop
{
    [DataContract]
    class SubDesktopItem
    {
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public int WindowHeight { get; set; }
        [DataMember]
        public string AppSubID { get; set; }
        [DataMember]
        public string UID { get; set; }
        [DataMember]
        public string HotKey { get; set; }
    }
}
