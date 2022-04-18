using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto.UserSite
{
    public class UserIndexDto
    {
        public IEnumerable<ProductDto> ParentProductDto { get; set; }
        public IEnumerable<ProductDto> ChildProductDto { get; set; }
        public ParameterClass ParameterClass { get; set; }
    }
}
