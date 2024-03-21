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
    }
}