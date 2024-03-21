using Microsoft.EntityFrameworkCore;
using ppedv.CubesPizza.Model;

namespace ppedv.CubesPizza.Data.EfCore
{
    public class PizzaContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Oven> Ovens { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }

        private string conString;

        public PizzaContext(string conString)
        {
            this.conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(conString);
        }
    }
}
