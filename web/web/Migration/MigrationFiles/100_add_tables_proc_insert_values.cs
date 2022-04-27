using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(200, "Add tables , proc,view and insert data")]
    public class _100_add_tables_proc_insert_values:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/100_add_tables.sql");
            Execute.Script(tablepath);

            string viewprocpath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/100_add_views_procs.sql");
            Execute.Script(viewprocpath);

            string insertvalues = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/100_InsertValues.sql");
            Execute.Script(insertvalues);
        }
    }
}