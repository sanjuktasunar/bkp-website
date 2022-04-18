using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class FiscalYear: BaseEntity
    {
        public int FiscalYearId { get; set; }
        public string FiscalYearName { get; set; }
        public string StartDateBS { get; set; }
        public DateTime StartDateAD { get; set; }
        public string EndDateBS { get; set; }
        public DateTime EndDateAD { get; set; }
        public bool IsCurrent { get; set; }
    }
}
