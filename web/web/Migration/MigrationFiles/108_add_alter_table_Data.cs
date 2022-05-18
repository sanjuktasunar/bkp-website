using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(208,"Add alter table for member approve,add")]
    public class _108_add_alter_table_Data:Migration
    {
        public override void Down()
        {
            //string downquery = System.Web.HttpContext.Current.Server.MapPath("/Query/108_query_down.sql");
            //Execute.Script(downquery);

            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/108_add_column_member.sql");
            Execute.Script(tablepath);
        }
    }
}