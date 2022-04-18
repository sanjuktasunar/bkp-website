using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity
{
    public class BaseDto
    {
        public bool? Status { get; set; }
    }
    public class BaseDtoData
    {
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class BaseDtoAll : BaseDtoData
    {
        public bool? Status { get; set; }
    }
}
