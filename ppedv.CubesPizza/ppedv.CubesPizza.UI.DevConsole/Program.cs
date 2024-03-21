using ppedv.CubesPizza.Core;
using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("🍕🍕🍕                  🍕🍕🍕");
Console.WriteLine("🍕🍕🍕 Cubes Pizza v0.1 🍕🍕🍕");
Console.WriteLine("🍕🍕🍕                  🍕🍕🍕");

string conString = "Server=(localdb)\\mssqllocaldb;Database=CubesPizza_Tests;Trusted_Connection=true;";

IRepository repo = new ppedv.CubesPizza.Data.EfCore.PizzaContextRepositoryAdapter(conString);
FoodService fs = new FoodService(repo);

Console.WriteLine("Speisekarte");
foreach (var p in fs.GetSpeisekarte())
{
    Console.WriteLine($"{p.Name} {p.Price:c} {(p.IsVegetarian ? "🥦" : "🍖")}");
}


Console.WriteLine("Bestellungen");
foreach (var o in repo.GetAll<Order>())
{
    Console.WriteLine($"{o.OrderDate:g} {o.CustomerName}");
}