using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(211, "add proc for member filter")]
    public class _111_add_customer_table:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/111_customer_query.sql");
            Execute.Script(tablepath);
        }
    }
}