using Microsoft.EntityFrameworkCore;
using RealEstateBE.Model;

namespace RealEstateBE.Data
{
    public class DataContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString =
              "Server=DESKTOP-HRQ6FH8; Database=RealEstateDB; Trusted_Connection=True;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connString);

        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyListingType> PropertyListingTypes { get; set; }
    }
}
