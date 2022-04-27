using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(206,"Add drop alter table")]
    public class _106_add_tables:Migration
    {
        public override void Down()
        {
            //string downquery = System.Web.HttpContext.Current.Server.MapPath("/Query/106_query_down.sql");
            //Execute.Script(downquery);
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/106_drop_create_insert_district.sql");
            Execute.Script(tablepath);
        }
    }
}