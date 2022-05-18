using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(202, "Alter view and proc")]
    public class _102_alter_proc_views:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string viewprocpath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/102_add_proc.sql");
            Execute.Script(viewprocpath);
        }
    }
}