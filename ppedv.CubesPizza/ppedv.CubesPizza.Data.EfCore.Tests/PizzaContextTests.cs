using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using ppedv.CubesPizza.Model;
using System.Reflection;

namespace ppedv.CubesPizza.Data.EfCore.Tests
{
    public class PizzaContextTests
    {
        string conString = "Server=(localdb)\\mssqllocaldb;Database=CubesPizza_Tests;Trusted_Connection=true;";

        [Fact]
        public void Can_create_new_database()
        {
            //Arrange
            var con = new PizzaContext(conString);
            con.Database.EnsureDeleted();

            //Act
            var result = con.Database.EnsureCreated();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Can_add_Pizza()
        {
            var con = new PizzaContext(conString);
            con.Database.EnsureCreated();
            var pizza = new Pizza() { Name = "Pizza 1", Price = 6.66m };

            con.Pizzas.Add(pizza);
            var rowCount = con.SaveChanges();

            Assert.Equal(1, rowCount);
        }

        [Fact]
        public void Can_read_Pizza()
        {
            var pizza = new Pizza() { Name = "Pizza 2", Price = 5.55m };
            using (var con = new PizzaContext(conString))
            {
                con.Database.EnsureCreated();
                con.Pizzas.Add(pizza);
                con.SaveChanges();
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                Assert.NotNull(loaded);
            }
        }

        [Fact]
        public void Can_update_Pizza()
        {
            var pizza = new Pizza() { Name = "Pizza 3", Price = 4.44m };
            decimal newPrice = 3.33m;
            using (var con = new PizzaContext(conString))
            {
                con.Database.EnsureCreated();
                con.Pizzas.Add(pizza);
                con.SaveChanges();
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                loaded.Price = newPrice;
                var rowCount = con.SaveChanges();
                Assert.Equal(1, rowCount);
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                Assert.Equal(newPrice, loaded.Price);
            }
        }

        [Fact]
        public void Can_delete_Pizza()
        {
            var pizza = new Pizza() { Name = "Pizza 4", Price = 2.22m };
            using (var con = new PizzaContext(conString))
            {
                con.Database.EnsureCreated();
                con.Pizzas.Add(pizza);
                con.SaveChanges();
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                con.Remove(loaded);
                var rowCount = con.SaveChanges();
                Assert.Equal(1, rowCount);
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                Assert.Null(loaded);
            }
        }


        [Fact]
        public void Can_create_and_read_Pizza_with_AutoFixture_and_FluentAssertions()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id), nameof(Entity.Created), nameof(Entity.Modified)));
            var pizza = fix.Create<Pizza>();

            using (var con = new PizzaContext(conString))
            {
                con.Database.EnsureCreated();
                con.Pizzas.Add(pizza);
                con.SaveChanges();
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                loaded.Should().BeEquivalentTo(pizza,x=>x.IgnoringCyclicReferences());
            }
        }
    }

    internal class PropertyNameOmitter : ISpecimenBuilder
    {
        private readonly IEnumerable<string> names;

        internal PropertyNameOmitter(params string[] names)
        {
            this.names = names;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;
            if (propInfo != null && names.Contains(propInfo.Name))
                return new OmitSpecimen();

            return new NoSpecimen();
        }
    }
}