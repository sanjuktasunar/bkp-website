using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(209,"data correction")]
    public class _109_data_correction:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/109_update_database_correct data.sql");
            Execute.Script(tablepath);
        }
    }
}