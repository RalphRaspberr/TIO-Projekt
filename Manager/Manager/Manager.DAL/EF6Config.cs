//using System;
//using System.CodeDom;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Manager.DAL
//{
//    public class EF6Config : DbConfiguration
//    {
//        public EF6Config()
//        {
            
//            this.SetDefaultConnectionFactory(new System.Data.Entity.Infrastructure.SqlConnectionFactory());
//            this.SetProviderServices("System.Data.SqlClient",
//                System.Data.Entity.SqlServer.SqlProviderServices.Instance);
//            this.SetDatabaseInitializer<ManagerContext>(new UsersInitializer());

//        }
//    }
//}
