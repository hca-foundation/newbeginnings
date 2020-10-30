using TNBCSurvey.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace TNBCSurvey.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<TNBCSurvey.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TNBCSurvey.Models.ApplicationDbContext context)
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
            context.Admin.AddOrUpdate(
                new Admin { Name = "c", Email = "c@c.com" },
                new Admin { Name = "b", Email = "b@b.com" },
                new Admin { Name = "z", Email = "z@z.com" }
            );
            context.Client.AddOrUpdate(
                new Client { FirstName = "cc", LastName = "dd", Email = "c@d.com", GroupNumber = 1, ProgramStartDate = Convert.ToDateTime("2020-07-10"), Active = true },
                new Client { FirstName = "aa", LastName = "bb",Email = "a@b.com", GroupNumber = 2, ProgramStartDate = Convert.ToDateTime("2020-03-22"), Active = true }
            );
        }
    }
}
