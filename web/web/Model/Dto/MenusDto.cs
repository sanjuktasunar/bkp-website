﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class MenusDto
    {
        public int MenuId { get; set; }
        public string MenuNameEnglish { get; set; }
        public string CheckMenuName { get; set; }
        public bool Status { get; set; }
    }
}
