using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Model
{
    public class DropdownList
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class KeyValuePairDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
