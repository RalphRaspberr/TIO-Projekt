using System.Collections.Generic;
using System.ServiceModel;
using UserService.Models;

namespace UserService
{
    [ServiceContract]
    public interface IUserRepository
    {
        [OperationContract]
        IEnumerable<User> GetAllUsers();

        [OperationContract]
        User GetUser(int id);

        [OperationContract]
        User AddUser(User u);

        [OperationContract]
        User DeleteUser(int id);

        [OperationContract]
        void Dispose();
    }
}
