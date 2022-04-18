using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NepaliName { get; set; }
        public bool? Status { get; set; }
        public bool? IsOutsideNepal { get; set; }
    }


    public class ProvinceDto
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceNameNepali { get; set; }
        public bool? Status { get; set; }
    }

    public class DistrictDto
    {
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameNepali { get; set; }
        public bool? Status { get; set; }
    }
}
