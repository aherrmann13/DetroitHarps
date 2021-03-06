﻿namespace DetroitHarps.DataAccess.Migrator
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.CommandLineUtils;

    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: false);
            app.Name = "Database Utilities";
            app.HelpOption("-?|-h|--help");

            app.Command("Create", (command) =>
                CommandLineApplicationHelper(command, x => x.Database.EnsureCreated()));

            app.Command("Delete", (command) =>
                CommandLineApplicationHelper(command, x => x.Database.EnsureDeleted()));

            app.Command("Migrate", (command) =>
                CommandLineApplicationHelper(command, x => x.Database.Migrate()));

            try
            {
                app.Execute(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void CommandLineApplicationHelper(
            CommandLineApplication app,
            Action<DetroitHarpsDbContext> action)
        {
            var dbContextFactory = new DetroitHarpsDbContextFactory();

            app.OnExecute(() =>
                {
                    using (var dbContext = dbContextFactory.CreateDbContext())
                    {
                        action.Invoke(dbContext);
                    }

                    return 0;
                });
        }
    }
}