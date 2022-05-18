using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(205, "add modify tables")]
    public class _105_add_modify_tables:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string viewprocpath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/105_add_column_add_table.sql");
            Execute.Script(viewprocpath);
        }
    }
}