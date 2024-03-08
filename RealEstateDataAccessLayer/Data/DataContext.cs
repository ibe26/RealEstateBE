using Microsoft.EntityFrameworkCore;
using RealEstateEntities.Entities;

namespace RealEstateDataAccessLayer.Data
{
    public class DataContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString =
              "Server=DESKTOP-HRQ6FH8; Database=RealEstateDB; Trusted_Connection=True;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connString);
            optionsBuilder.UseLazyLoadingProxies();

        }
        public DataContext()
        {
            
        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyListingType> PropertyListingTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OwnedProperty> OwnedProperties { get; set; }
    }
}
