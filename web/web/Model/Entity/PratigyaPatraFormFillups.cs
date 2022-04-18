using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class PratigyaPatraFormFillups
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int AppliedKitta { get; set; }
        public bool? IsFillup { get; set; }
    }
}
