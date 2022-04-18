using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Infrastructure
{
    public class Response
    {
        public string messageType { get; set; }
        public string message { get; set; }
        public int id { get; set; }
        public List<string> messageList { get; set; }
    }

    public class ImageResponse
    {
        public string messageType { get; set; }
        public string message { get; set; }
        public string imageBase64String { get; set; }
    }
}
