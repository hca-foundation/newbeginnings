using TNBCSurvey.Models;

namespace TNBCSurvey.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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
                new Client { FirstName = "cc", LastName = "dd", Email = "c@d.com", Phone = "1234567890", GroupNumber = "1", Survey_Status = "Pending" },
                new Client { FirstName = "aa", LastName = "bb",Email = "a@b.com",Phone = "1234567890", GroupNumber = "2", Survey_Status = "Pending" }
            );
        }
    }
}
