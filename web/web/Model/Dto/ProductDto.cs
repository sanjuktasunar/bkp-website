using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class ProductDto:BaseDtoAll
    {
        public int ProductId { get; set; }
        public int? ParentProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductNameNepali { get; set; }
        public string ProductCode { get; set; }

        public string ParentProductName { get; set; }
        public string ParentProductNameNepali { get; set; }

        public byte[] Photo { get; set; }
        public decimal SellingPrice { get; set; }

        public IEnumerable<ProductDto> GetActiveParentProduct { get; set; }
        public IEnumerable<ProductDto> ChildProducts { get; set; }
        public IEnumerable<ProductPriceDto> GetProductPrice { get; set; }

        public IEnumerable<UnitDto> Units { get; set; }
        public IEnumerable<ProductImageDto> ProductImages { get; set; }
        public ProductImageDto ProductImage { get; set; }
    }
}
