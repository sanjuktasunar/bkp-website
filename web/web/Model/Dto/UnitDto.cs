using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class UnitDto:BaseDtoAll
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string UnitSymbol { get; set; }
        public string UnitSymbolNepali { get; set; }
        public string UnitNameNepali { get; set; }
    }
}
