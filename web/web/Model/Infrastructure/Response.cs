using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;

namespace Web.Entity.Infrastructure
{
    public class Response
    {
        public string messageType { get; set; }
        public string message { get; set; }
        public int id { get; set; }
        public string value { get; set; }
        public List<string> messageList { get; set; }
    }

    public class MemberResponse
    {
        public string messageType { get; set; }
        public string message { get; set; }
        public int id { get; set; }
        public string value { get; set; }
        public MemberDto memberDto { get; set; }
        public UserDocumentDto userDocumentDto { get; set; }
        public List<string> messageList { get; set; }
    }

    public class ImageResponse
    {
        public string messageType { get; set; }
        public string message { get; set; }
        public string imageBase64String { get; set; }
    }
}
