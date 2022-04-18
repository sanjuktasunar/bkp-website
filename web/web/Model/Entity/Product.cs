using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class Product:BaseEntityAll
    {
        public int ProductId { get; set; }
        public int? ParentProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductNameNepali { get; set; }
    }
}
