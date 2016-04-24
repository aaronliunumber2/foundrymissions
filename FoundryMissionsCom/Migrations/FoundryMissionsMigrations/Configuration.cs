namespace FoundryMissionsCom.Migrations.FoundryMissionsMigrations
{
    using Helpers;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FoundryMissionsCom.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\FoundryMissionsMigrations";
        }

        protected override void Seed(FoundryMissionsCom.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //add the mission tag types


            SeedDataHelper.AddUsersAndRoles(context);
            context.MissionTagTypes.AddOrUpdate(
                m => m.TagName,
                SeedDataHelper.GetMissionTagTypes().ToArray(),
            );


            context.SaveChanges();
        }
    }
}
