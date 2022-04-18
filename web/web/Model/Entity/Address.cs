using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class Address
    {
        public int Id { get; set; }
        public int? MemberId { get; set; }
        public bool? PermanentIsOutsideNepal { get; set; }

        public int? PermanentProvinceId { get; set; }
        public int? PermanentDistrictId { get; set; }
        public int? PermanentMunicipalityTypeId { get; set; }
        public string PermanentMunicipality { get; set; }
        public string PermanentWardNumber { get; set; }
        public string PermanentToleName { get; set; }

        public int? PermanentCountryId { get; set; }
        public string PermanentAddress { get; set; }

        public bool? TemporaryIsOutsideNepal { get; set; }
        public int? TemporaryProvinceId { get; set; }
        public int? TemporaryDistrictId { get; set; }
        public int? TemporaryMunicipalityTypeId { get; set; }
        public string TemporaryMunicipality { get; set; }
        public string TemporaryWardNumber { get; set; }
        public string TemporaryToleName { get; set; }

        public int? TemporaryCountryId { get; set; }
        public string TemporaryAddress { get; set; }
    }
}
