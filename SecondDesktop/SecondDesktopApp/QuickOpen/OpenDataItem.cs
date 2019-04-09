using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QuickOpen
{
    [DataContract]
    public class OpenDataItem
    {
        [DataMember]
        public int UID
        {
            get;
            set;
        }

        [DataMember]
        public string Title
        {
            get;
            set;
        }

        [DataMember]
        public string Path
        {
            get;
            set;
        }
    }
}
