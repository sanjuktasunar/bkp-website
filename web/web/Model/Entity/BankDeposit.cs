using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class BankDeposit
    {
        public int Id { get; set; }
        public int? MemberId { get; set; }
        public bool? IsOther { get; set; }
        public bool? IsVoucherDeposit { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public int? AccountHeadId { get; set; }
        public DateTime? DepositDate { get; set; }
        public string VoucherImage { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }

    }
}
