using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using ModernStore.Domain.Entities;
using ModernStore.Infra.Mappings;
using ProviderService = System.Data.Entity.SqlServer.SqlProviderServices;

namespace ModernStore.Infra.Contexts
{
    public class ModernStoreDataContext : DbContext
    {
        public ModernStoreDataContext() : base(@"Data Source=DESKTOP-9MGSMES;Initial Catalog=ModernStore;Integrated Security = true;")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false; 
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new OrderItemMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new UserMap());
        }

        private void ResolveDependence()
        {
            var instanceDependencia = ProviderService.Instance;
        }
    }
}
