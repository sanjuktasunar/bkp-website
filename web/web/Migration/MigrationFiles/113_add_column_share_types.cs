using System;
using FluentMigrator;


namespace web.Migrations
{
    [Migration(213, "add column in share types")]
    public class _113_add_column_share_types:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/113_alter_colum_share_type.sql");
            Execute.Script(tablepath);
        }
    }
}