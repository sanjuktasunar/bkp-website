using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Infrastructure
{
    public class AddressWise
    {
        public int? ProvinceId { get; set; }

        public int? DistrictId { get; set; }

        public int? GenderId { get; set; }

        public int? CountryId { get; set; }

        public int OutsideCountry { get; set; }

        public int? UserId { get; set; }

        public bool IsAdmin { get; set; }

    }
}
