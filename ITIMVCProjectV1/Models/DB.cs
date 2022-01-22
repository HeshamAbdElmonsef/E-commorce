using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ITIMVCProjectV1.Models
{
    public partial class DB : DbContext
    {
        public DB()
            : base("name=DB")
        {
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SubOrder> SubOrders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feedback> Feadbacks { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
