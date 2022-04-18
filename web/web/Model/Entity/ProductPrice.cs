using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class ProductPrice:BaseEntity
    {
        public int ProductPriceId { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public float SellingPrice { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsPrimary { get; set; }
    }
}
