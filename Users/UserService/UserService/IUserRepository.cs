using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UserService.DAL;
using UserService.Models;

namespace UserService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IUserRepository
    {
        [OperationContract]
        IQueryable<User> GetAllUsers();

        [OperationContract]
        User GetSingleUser(int id);

        [OperationContract]
        bool AddUser(User u);

        [OperationContract]
        bool DeleteUser(int id);

        // TO-DO
        //[OperationContract]
        //User UpdateUser(User u);
    }
}
