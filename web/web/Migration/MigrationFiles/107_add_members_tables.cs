using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(207, "Add alter table for members,insert values")]
    public class _107_add_members_tables:Migration
    {
        public override void Down()
        {
            //string downquery = System.Web.HttpContext.Current.Server.MapPath("/Query/107_query_down.sql");
            //Execute.Script(downquery);

            throw new NotImplementedException();
        }

        public override void Up()
        {
            string tablepath = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/107_create_Member_Registration.sql");
            Execute.Script(tablepath);

            string insertValue = System.Web.HttpContext.Current.Server.MapPath("/Migration/Query/107_insert_value_member_registration.sql");
            Execute.Script(insertValue);
        }
    }
}