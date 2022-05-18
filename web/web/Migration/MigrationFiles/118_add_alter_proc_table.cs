using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(218, "add alter proc table")]
    public class _118_add_alter_proc_table: Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/118_add_alter_view.sql");
            Execute.Script(tablepath);
        }
    }
}