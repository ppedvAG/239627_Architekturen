using Moq;
using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace ppedv.CubesPizza.Core.Tests
{
    public class FoodServiceTests
    {
        [Fact]
        public void GetSpeisekarte_should_return_3_Pizzas()
        {
            var fs = new FoodService(new TestUoW());

            var result = fs.GetSpeisekarte();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetSpeisekarte_should_return_3_Pizzas_Mock()
        {
            
            var mock = new Mock<IRepository<Pizza>>();
            mock.Setup(x => x.GetAll()).Returns(() =>
            {
                var p1 = new Pizza() { Name = "P1", Price = 4.99m };
                var p2 = new Pizza() { Name = "P2", Price = 5.99m, IsVegetarian = false };
                var p3 = new Pizza() { Name = "P3", Price = 6.99m };

                return new[] { p1, p2, p3 };
            });
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.FoodRepository).Returns(mock.Object);
            var fs = new FoodService(uowMock.Object);

            var result = fs.GetSpeisekarte();

            Assert.Equal(3, result.Count());

            mock.Verify(x => x.GetAll(), Times.Exactly(1));
        }

        //[Fact]
        //public void GetSpeisekarte_ReturnsAllPizzas_WhenNurVegetarischIsFalse()
        //{
        //    // Arrange
        //    var pizzas = new List<Pizza>
        //    {
        //        new Pizza { Name = "Hawaii", Price = 8.99m, IsVegetarian = false },
        //        new Pizza { Name = "Margherita", Price = 5.99m, IsVegetarian = true },
        //        new Pizza { Name = "Salami", Price = 7.99m, IsVegetarian = false }
        //    };
        //    var mockRepository = new Mock<PizzaContextUnitOfWorkAdapter>();
        //    mockRepository.Setup(r => r.GetAll<Pizza>()).Returns(pizzas);

        //    var foodService = new FoodService(mockRepository.Object);

        //    // Act
        //    var result = foodService.GetSpeisekarte().ToList();

        //    // Assert
        //    Assert.Equal(3, result.Count);
        //    Assert.Equal("Margherita", result[0].Name); // Erwartet, dass die Pizzen nach dem Preis sortiert sind
        //    Assert.Equal("Salami", result[1].Name);
        //    Assert.Equal("Hawaii", result[2].Name);
        //}

        //[Fact]
        //public void GetSpeisekarte_ReturnsOnlyVegetarianPizzas_WhenNurVegetarischIsTrue()
        //{
        //    // Arrange
        //    var pizzas = new List<Pizza>
        //    {
        //    new Pizza { Name = "Hawaii", Price = 8.99m, IsVegetarian = false },
        //    new Pizza { Name = "Margherita", Price = 7.99m, IsVegetarian = true },
        //    new Pizza { Name = "Veggie", Price = 6.99m, IsVegetarian = true }
        //    };
        //    var mockRepository = new Mock<IRepository>();
        //    mockRepository.Setup(r => r.GetAll<Pizza>()).Returns(pizzas);

        //    var foodService = new FoodService(mockRepository.Object);

        //    // Act
        //    var result = foodService.GetSpeisekarte(nurVegetarisch: true).ToList();

        //    // Assert
        //    Assert.Equal(2, result.Count); // Nur vegetarische Pizzen
        //    Assert.True(result.All(x => x.IsVegetarian)); // Stellt sicher, dass alle Pizzen vegetarisch sind
        //    Assert.Equal("Veggie", result[0].Name); // Erwartet, dass die Pizzen nach dem Preis sortiert sind
        //    Assert.Equal("Margherita", result[1].Name);
        //}
    }

    public class TestUoW : IUnitOfWork
    {
        public IRepository<Pizza> FoodRepository => new TestRepo();

        public IRepository<Address> AddressRepository => throw new NotImplementedException();

        public IOrderRepository OrderRepository => throw new NotImplementedException();

        public void SaveAll()
        {
            throw new NotImplementedException();
        }
    }

    public class TestRepo : IRepository<Pizza>
    {
        public void Add(Entity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Entity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pizza> GetAll()
        {


            var p1 = new Pizza() { Name = "P1", Price = 4.99m };
            var p2 = new Pizza() { Name = "P2", Price = 5.99m, IsVegetarian = false };
            var p3 = new Pizza() { Name = "P3", Price = 6.99m };

            return new[] { p1, p2, p3 };

        }

        public Pizza? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}