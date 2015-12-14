using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace RIAT2
{
     [DataContract]
    class Input
    {
        [DataMember(Order = 1)]
        public int K { get; set; }
        [DataMember(Order = 2)]
        public decimal[] Sums { get; set; }
        [DataMember(Order = 3)]
        public int[] Muls { get; set; }
    }
}
