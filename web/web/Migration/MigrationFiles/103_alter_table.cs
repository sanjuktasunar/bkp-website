using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(203, "alter table")]
    public class _103_alter_table:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string viewprocpath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/103_alter_table.sql");
            Execute.Script(viewprocpath);
        }
    }
}