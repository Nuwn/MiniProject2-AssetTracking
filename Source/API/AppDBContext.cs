using DataModels;
using Microsoft.EntityFrameworkCore;

namespace API
{
    internal class AppDBContext : DbContext
    {
        private static AppDBContext? singleton;
        public static AppDBContext Get => singleton ??= new AppDBContext();

        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EfRelationships1;Trusted_Connection=True;MultipleActiveResultSets=true";
        
        public DbSet<Office> Offices { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Asset> Assets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between Assets and Offices
            modelBuilder.Entity<Asset>()
                .HasOne(a => a.Office)
                .WithMany(o => o.Assets)
                .IsRequired();

            // Configure the inheritance relationship between Devices and Computers
            modelBuilder.Entity<Computer>()
                .HasBaseType<Device>();

            // Configure the inheritance relationship between Devices and Mobiles
            modelBuilder.Entity<Mobile>()
                .HasBaseType<Device>();


            // Seed the database with data
            modelBuilder.Entity<Office>().HasData(
                new Office { Id = 1, Name = "Tokyo", Currency = "JPY" },
                new Office { Id = 2, Name = "Oslo", Currency = "NOK" },
                new Office { Id = 3, Name = "London", Currency = "GBP" }
            );
            modelBuilder.Entity<Mobile>().HasData(
                new Mobile { Id = 1, Brand = "Samsung", Model = "S12" },
                new Mobile { Id = 2, Brand = "IPhone", Model = "11" }
            );
            modelBuilder.Entity<Computer>().HasData(
                new Computer { Id = 3, Brand = "ASUS", Model = "5563" },
                new Computer { Id = 4, Brand = "Lenovo" , Model = "G56" }
            );
            modelBuilder.Entity<Asset>().HasData(
                new Asset { Id = 1, OfficeId = 1, DeviceId = 1, PurchaseDate = new DateTime(2022,02,15), Price = 1999.90m },
                new Asset { Id = 2, OfficeId = 2, DeviceId = 2, PurchaseDate = new DateTime(2021, 06, 15), Price = 3099.90m },
                new Asset { Id = 3, OfficeId = 3, DeviceId = 3, PurchaseDate = new DateTime(2022, 12, 06), Price = 5199.90m },
                new Asset { Id = 4, OfficeId = 3, DeviceId = 4, PurchaseDate = new DateTime(2020, 02, 15), Price = 999.90m }
            );
        }
    }
}
