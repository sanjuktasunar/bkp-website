﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class UserStatusDto
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public bool? Status { get; set; }
    }
}
