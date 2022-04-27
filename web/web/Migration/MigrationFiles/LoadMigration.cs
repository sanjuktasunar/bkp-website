using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SqlServer;

namespace web.Migrations
{
    public class LoadMigration
    {
        public void Load()
        {
            Announcer announcer = new TextWriterAnnouncer(x => Debug.WriteLine(""));
            var assembly = Assembly.GetExecutingAssembly();
            IRunnerContext ctx = new RunnerContext(announcer);
            var options = new ProcessorOptions
            {
                PreviewOnly = false,
                Timeout = 40
            };
            var factory = new SqlServer2012ProcessorFactory();
            string con = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            var processer = factory.Create(con, announcer, options);

            var runner = new MigrationRunner(assembly, ctx, processer);
            runner.MigrateUp();
        }
    }
}