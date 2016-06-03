using System.Collections.Generic;
using System.Security.Cryptography;
using ExtensionMethods;
using UserService.Models;

namespace UserService.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UserService.DAL.ServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UserService.DAL.ServiceContext context)
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
                    PasswordSHA1 = "hababa?".MySHA()
                },
                new User()
                {
                    Nick = "Matimati",
                    PasswordSHA1 = "habababa!!".MySHA()
                },
                new User()
                {
                    Nick = "Matimatimati",
                    PasswordSHA1 = "hababababa!!".MySHA()
                }
            );
            context.SaveChanges();
        }
    }
}
