using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Entity.Dto
{
    public class EmailDto
    {
        public string ToEmail { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
