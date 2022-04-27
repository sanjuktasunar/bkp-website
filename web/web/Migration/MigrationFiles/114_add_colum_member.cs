using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(214, "add column in member table")]
    public class _114_add_colum_member:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/114_add_column_member.sql");
            Execute.Script(tablepath);
        }
    }
}