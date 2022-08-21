using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Web.Entity.Dto
{

    public class DropdownRequestDto
    {
        public string type { get; set; }
        public int? provinceId { get; set; }
    }

    public class DropdownDto
    {
        public int Key { get; set; }
        public string Value { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }
}