using IPAddressManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Cryptography.X509Certificates;

namespace IPAddressManagement.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
           
        }

        public DbSet<IPAddresss> IPAddressses { get; set; }
        public DbSet<RentalContract> RentalContracts { get; set; }
        public DbSet<GroupUser> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IPAddresss>()
                .HasIndex(u => u.IPAddressName)
                .IsUnique();
            builder.Entity<GroupUser>()
                .HasIndex(u => u.Name)
                .IsUnique();
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            builder.Entity<Customer>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();
        }
    }
}

    

    
    

