using System;
using System.Collections.Generic;
using System.ServiceModel;
using UserService.DAL;
using UserService.Models;

namespace UserService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class UserRepository : IUserRepository, IDisposable
    {
        private ServiceContext db = new ServiceContext();

        public IEnumerable<User> GetAllUsers()
        {
            return db.Users;
        }

        public User GetUser(int id)
        { 
            return db.Users.Find(id);
        }

        public User AddUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
            return u;
        }

        public User DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return user;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
