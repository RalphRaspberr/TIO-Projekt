using System.Security.Cryptography;
using Manager.Models;

namespace Manager.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Manager.DAL.ManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Manager.DAL.ManagerContext context)
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

            context.Users.AddOrUpdate(
                new User()
                {
                    Nick = "Mati",
                    PasswordSHA1 = SHA1.Create("hababa?").ToString()
                },
                new User()
                {
                    Nick = "Matimati",
                    PasswordSHA1 = SHA1.Create("habababa!!").ToString()
                }
            );
        }
    }
}
