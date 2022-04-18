﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class ProductImage
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public byte[] Photo { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsPrimary { get; set; }
    }
}
