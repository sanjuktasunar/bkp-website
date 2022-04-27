using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(217, "add proc add view")]
    public class _117_addproc_Add_view:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/117_alterproc_alter_view.sql");
            Execute.Script(tablepath);
        }
    }
}