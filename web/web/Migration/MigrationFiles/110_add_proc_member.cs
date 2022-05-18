using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(210, "add proc for member filter")]
    public class _110_add_proc_member:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/110_add_proc.sql");
            Execute.Script(tablepath);
        }
    }
}