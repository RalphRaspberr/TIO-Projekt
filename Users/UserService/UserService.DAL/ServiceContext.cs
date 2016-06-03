using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UserService.Models;

namespace UserService.DAL
{
    public class ServiceContext : DbContext 
    {
        public ServiceContext() : base("SOA.UserService")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
