using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(216, "add column AppliedKitta in member table")]
    public class _116_add_pratigyapatra:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/116_add_pratigyapatra_details.sql");
            Execute.Script(tablepath);
        }
    }
}