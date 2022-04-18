using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class PhotoStoragesDto
    {
        public int PhotoStorageId { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoLocation { get; set; }
    }
}
