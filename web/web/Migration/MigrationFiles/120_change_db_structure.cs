using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(220, "change db structure")]
    public class _120_change_db_structure : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/120_change_in_db_structure.sql");
            Execute.Script(tablepath);
        }
    }
}