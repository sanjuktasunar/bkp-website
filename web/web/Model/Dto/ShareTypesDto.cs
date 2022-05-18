using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class ShareTypesDto
    {
        public int ShareTypeId { get; set; }

        public string ShareTypeName { get; set; }

        public int FiscalYearId { get; set; }

        public long? NumberOfIssuedShares { get; set; }

        public int? MaxSharePerPerson { get; set; }

        public int? MinSharePerPerson { get; set; }

        public bool? Status { get; set; }

        public bool IsPrimary { get; set; }

        public string FiscalYearName { get; set; }

        public double? PricePerKitta { get; set; }

        public double RegistrationAmount { get; set; }
    }
}
