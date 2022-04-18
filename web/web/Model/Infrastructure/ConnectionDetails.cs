using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace web.Web.Entity.Infrastructure
{
    public class SqlConnectionDetails
    {
        public SqlConnection conn { get; set; }
        public IDbTransaction trans { get; set; }

    }
}