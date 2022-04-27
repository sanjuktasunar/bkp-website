using System;
using FluentMigrator;
namespace web.Migrations
{
    [Migration(201, "Add view")]
    public class _101_AddViews:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string viewprocpath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/101_add_view.sql");
            Execute.Script(viewprocpath);
        }
    }
}