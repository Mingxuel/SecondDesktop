using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppDemo
{
    [DataContract]
    class SubAppData
    {
        public SubAppData() {

        }

        [DataMember]
        public string TestText
        {
            get;
            set;
        }
    }
}
