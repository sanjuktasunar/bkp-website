using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(219, "alter member view")]
    public class _119_alter_member_view: Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/119_alter_view.sql");
            Execute.Script(tablepath);
        }
    }
}