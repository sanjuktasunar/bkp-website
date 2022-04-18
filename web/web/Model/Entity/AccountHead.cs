using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class AccountHead
    {
        public int AccountHeadId { get; set; }
        public string AccountHeadName { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public string Address { get; set; }
        public bool? Status { get; set; }
    }
}
