using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace RIAT2
{
       [DataContract]
    class Output
    {
        [DataMember(Order = 1)]
        public decimal SumResult { get; set; }
        [DataMember(Order = 2)]
        public int MulResult { get; set; }
        [DataMember(Order = 3)]
        public decimal[] SortedInputs { get; set; }
        public Output()
        {
            this.SumResult = 0;
            this.MulResult = 1;
        }

    }
}
