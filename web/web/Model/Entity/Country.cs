using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NepaliName { get; set; }
        public bool? Status { get; set; }
        public bool? IsOutsideNepal { get; set; }
    }
}
