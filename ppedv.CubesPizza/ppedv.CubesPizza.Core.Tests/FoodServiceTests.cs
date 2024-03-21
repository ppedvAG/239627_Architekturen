using Moq;
using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;

namespace ppedv.CubesPizza.Core.Tests
{
    public class FoodServiceTests
    {
        [Fact]
        public void GetSpeisekarte_should_return_3_Pizzas()
        {
            var fs = new FoodService(new TestRepo());

            var result = fs.GetSpeisekarte();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetSpeisekarte_should_return_3_Pizzas_Mock()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Pizza>()).Returns(() =>
            {
                var p1 = new Pizza() { Name = "P1", Price = 4.99m };
                var p2 = new Pizza() { Name = "P2", Price = 5.99m, IsVegetarian = false };
                var p3 = new Pizza() { Name = "P3", Price = 6.99m };

                return new[] { p1, p2, p3 };
            });
            var fs = new FoodService(mock.Object);

            var result = fs.GetSpeisekarte();

            Assert.Equal(3, result.Count());

            mock.Verify(x => x.GetAll<Pizza>(), Times.Exactly(1));
        }

        [Fact]
        public void GetSpeisekarte_ReturnsAllPizzas_WhenNurVegetarischIsFalse()
        {
            // Arrange
            var pizzas = new List<Pizza>
            {
                new Pizza { Name = "Hawaii", Price = 8.99m, IsVegetarian = false },
                new Pizza { Name = "Margherita", Price = 5.99m, IsVegetarian = true },
                new Pizza { Name = "Salami", Price = 7.99m, IsVegetarian = false }
            };
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(r => r.GetAll<Pizza>()).Returns(pizzas);

            var foodService = new FoodService(mockRepository.Object);

            // Act
            var result = foodService.GetSpeisekarte().ToList();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("Margherita", result[0].Name); // Erwartet, dass die Pizzen nach dem Preis sortiert sind
            Assert.Equal("Salami", result[1].Name);
            Assert.Equal("Hawaii", result[2].Name);
        }

        [Fact]
        public void GetSpeisekarte_ReturnsOnlyVegetarianPizzas_WhenNurVegetarischIsTrue()
        {
            // Arrange
            var pizzas = new List<Pizza>
            {
            new Pizza { Name = "Hawaii", Price = 8.99m, IsVegetarian = false },
            new Pizza { Name = "Margherita", Price = 7.99m, IsVegetarian = true },
            new Pizza { Name = "Veggie", Price = 6.99m, IsVegetarian = true }
            };
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(r => r.GetAll<Pizza>()).Returns(pizzas);

            var foodService = new FoodService(mockRepository.Object);

            // Act
            var result = foodService.GetSpeisekarte(nurVegetarisch: true).ToList();

            // Assert
            Assert.Equal(2, result.Count); // Nur vegetarische Pizzen
            Assert.True(result.All(x => x.IsVegetarian)); // Stellt sicher, dass alle Pizzen vegetarisch sind
            Assert.Equal("Veggie", result[0].Name); // Erwartet, dass die Pizzen nach dem Preis sortiert sind
            Assert.Equal("Margherita", result[1].Name);
        }
    }


    public class TestRepo : IRepository
    {
        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            if (typeof(T) == typeof(Pizza))
            {
                var p1 = new Pizza() { Name = "P1", Price = 4.99m };
                var p2 = new Pizza() { Name = "P2", Price = 5.99m, IsVegetarian = false };
                var p3 = new Pizza() { Name = "P3", Price = 6.99m };

                return new[] { p1, p2, p3 }.Cast<T>();
            }

            throw new NotImplementedException();
        }

        public void Add<T>(Entity entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(Entity entity) where T : Entity
        {
            throw new NotImplementedException();
        }



        public T? GetById<T>(int id) where T : Entity
        {
            throw new NotImplementedException();
        }

        public void SaveAll()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(Entity entity) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}