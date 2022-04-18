using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity
{
    public class BaseEntity
    {
        public bool? Status { get; set; }
    }

    public class BaseEntityData
    {
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class BaseEntityAll : BaseEntityData
    {
        public bool? Status { get; set; }
    }
}
