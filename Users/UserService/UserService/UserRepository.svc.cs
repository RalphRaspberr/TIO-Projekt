using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.ModelBinding;
using UserService.DAL;
using UserService.Models;

namespace UserService
{
    public class UserRepository : IUserRepository
    {
        private ServiceContext db = new ServiceContext();

        public IQueryable<User> GetAllUsers()
        {
            return db.Users;
        }

        public User GetSingleUser(int id)
        {
            return db.Users.Find(id);
        }

        public bool AddUser(User u)
        {
            try
            {
                db.Users.Add(u);
                db.SaveChanges();
            }
            catch (SystemException sE)
            {
                Console.WriteLine(sE.Message);
                return false;
            }
            return true;
        }

        bool IUserRepository.DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            catch (SystemException sE)
            {
                Console.WriteLine(sE.Message);
                return false;
            }
            return true;
        }
    }
}
