using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SecondDesktop
{
    [DataContract]
    class SubDesktop
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Page { get; set; }
        [DataMember]
        public List<SubDesktopItem> SubDesktopItemList { get; set; }
    }
}
