using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class MemberFieldDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NealiName { get; set; }
        public bool? Status { get; set; }
    }

    public class OccupationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NealiName { get; set; }
        public bool? Status { get; set; }
    }
}
