using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Security.Cryptography;
using UserService.Models;
using ExtensionMethods;

namespace UserService.DAL
{
    public class UsersInitializer : DropCreateDatabaseIfModelChanges<ServiceContext>
    {
        protected override void Seed(ServiceContext context)
        {
            SeedUsers(context);
            base.Seed(context);
        }

        private void SeedUsers(ServiceContext context)
        {
            var users = new List<User>
            {
                new User()
                {
                    Nick = "Mati2",
                    PasswordSHA1 = "hababa?".MySHA()
                },
                new User()
                {
                    Nick = "Matimati2",
                    PasswordSHA1 = "habababa!!".MySHA()
                }
            };
            users.ForEach(u => context.Users.AddOrUpdate(u));
            context.SaveChanges();
        }
    }
}
