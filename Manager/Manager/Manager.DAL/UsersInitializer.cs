using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Manager.Models;

namespace Manager.DAL
{
    public class UsersInitializer : DropCreateDatabaseAlways<ManagerContext>
    {
        protected override void Seed(ManagerContext context)
        {
            SeedUsers(context);
            base.Seed(context);
        }

        private void SeedUsers(ManagerContext context)
        {
            var users = new List<User>
            {
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
            };
            users.ForEach(u => context.Users.AddOrUpdate(u));
            context.SaveChanges();
        }
    }
}
