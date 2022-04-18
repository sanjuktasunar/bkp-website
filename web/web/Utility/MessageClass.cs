using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Entity.Infrastructure;

namespace web.Utility
{
    public class MessageClass
    {
        public Response SaveMessage(int result)
        {
            var resp = new Response();
            if (result >= 0)
            {
                resp.messageType = "success";
                resp.message = "Data Saved SuccessFully";
            }
            else
            {
                resp.messageType = "error";
                resp.message = "Error!!! Data Cannot be Saved";
            }
            return resp;
        }

        public Response DeleteMessage(int result)
        {
            var resp = new Response();
            if (result >= 0)
            {
                resp.messageType = "success";
                resp.message = "Data Removed SuccessFully";
            }
            else
            {
                resp.messageType = "error";
                resp.message = "Error !!! Data Cannot be Deleted";
            }
            return resp;
        }

        public Response NotFoundMessage()
        {
            var resp = new Response();
            resp.messageType = "error";
            resp.message = "Error !!! Data Cannot be Found";
            return resp;
        }

        public Response ClearAllCacheMessage()
        {
            var resp = new Response();
            resp.messageType = "success";
            resp.message = "Success !!! Cache cleared successfully";
            return resp;
        }
    }
}