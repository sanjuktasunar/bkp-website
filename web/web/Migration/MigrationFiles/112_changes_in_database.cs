using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(212, "add proc for member filter")]
    public class _112_changes_in_database:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/112_add_table.sql");
            Execute.Script(tablepath);
        }
    }
}