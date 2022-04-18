using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class Staffs
    {
        public int StaffId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public string StaffName { get; set; }
        public int GenderId { get; set; }
        public string TemporaryAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string CitizenshipNumber { get; set; }
        public string PanNumber { get; set; }
        public float BasicSalary { get; set; }
    }
}
