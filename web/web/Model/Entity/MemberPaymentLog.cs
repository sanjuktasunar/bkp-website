﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Entity.Entity
{
    public class MemberPaymentLog
    {
        public int PaymentId { get; set; }
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }
    }
}