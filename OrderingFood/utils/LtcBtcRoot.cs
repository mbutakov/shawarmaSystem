using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrderingFood.utils
{ 
    [DataContract]
    class LtcBtcRoot
    {
        [DataMember(Name = "Dish")]
        public LtcBtc[] Items { get; set; }
    }

    [DataContract]
    class LtcBtc
    {
        [DataMember(Name = "ID")]
        public string Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Price")]
        public string Price { get; set; }

    }
}
