using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Manager.Models;

namespace Manager.DAL
{
    //[DbConfigurationType(typeof(EF6Config))]
    public class ManagerContext : DbContext 
    {
        public ManagerContext() : base("ManagerContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<ManagerContext>(new UsersInitializer());
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
