using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(221, "change db structure")]
    public class _121_add_alter_table_views_procs : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/121_add_modify_table_procs.sql");
            Execute.Script(tablepath);
        }
    }
}