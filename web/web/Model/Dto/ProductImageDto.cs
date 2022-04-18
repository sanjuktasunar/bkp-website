using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web.Entity.Dto
{
    public class ProductImageDto
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public byte[] Photo { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsPrimary { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public string ImageString { get; set; }
    }
}
