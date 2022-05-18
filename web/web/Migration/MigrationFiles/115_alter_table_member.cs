using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(215, "add column AppliedKitta in member table")]
    public class _115_alter_table_member : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/115_alter_table_member.sql");
            Execute.Script(tablepath);
        }
    }
}