using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class CustomerInfo:BaseEntityAll
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public string ContactNumber { get; set; }
        public string ContactNumber1 { get; set; }
        public string EmailAddress { get; set; }
        public string PanNumber { get; set; }
    }
}
