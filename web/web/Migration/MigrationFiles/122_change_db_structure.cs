using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(222,"Change DB Structure")]
    public class _122_change_db_structure : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/122_change_table_format.sql");
            Execute.Script(tablepath);
        }
    }
}