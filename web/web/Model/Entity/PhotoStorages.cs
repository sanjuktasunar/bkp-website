using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class PhotoStorages
    {
        public int PhotoStorageId { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoLocation { get; set; }
    }
}
