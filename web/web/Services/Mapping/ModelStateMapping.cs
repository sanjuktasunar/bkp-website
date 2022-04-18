using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Entity.Model;

namespace Web.Services.Mapping
{
    public static class ModelStateMapping
    {
        public static KeyValuePairDto ToModelState(this KeyValuePair<string,ModelState> keyValuePair)
        {
            var obj= new KeyValuePairDto
            {
                Key = keyValuePair.Key,
                Value= keyValuePair.Value.Errors.Select(a=>a.ErrorMessage).FirstOrDefault(),
            };
            return obj;
        }
    }
}
