using Autofac;
using Autofac.Core;
using ppedv.CubesPizza.Core;
using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;
using System.Reflection;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("🍕🍕🍕                  🍕🍕🍕");
Console.WriteLine("🍕🍕🍕 Cubes Pizza v0.1 🍕🍕🍕");
Console.WriteLine("🍕🍕🍕                  🍕🍕🍕");

string conString = "Server=(localdb)\\mssqllocaldb;Database=CubesPizza_Tests;Trusted_Connection=true;";


//DI per Hand
//IRepository repo = new ppedv.CubesPizza.Data.EfCore.PizzaContextRepositoryAdapter(conString);
//FoodService fs = new FoodService(repo);

//DI per Reflection
//var pathToLib = @"C:\Users\Fred\source\repos\ppedvAG\239627_Architekturen\ppedv.CubesPizza\ppedv.CubesPizza.Data.EfCore\bin\Debug\net8.0\ppedv.CubesPizza.Data.EfCore.dll";
//var ass = Assembly.LoadFrom(pathToLib);
//var typMitRepo = ass.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IRepository)));
//var repo = (IRepository)Activator.CreateInstance(typMitRepo,pathToLib);
//FoodService fs = new FoodService(repo);

//DI per AutoFac
var builder = new ContainerBuilder();
builder.RegisterType<FoodService>();
builder.Register(x => new ppedv.CubesPizza.Data.EfCore.PizzaContextRepositoryAdapter(conString)).As<IRepository>();
var container = builder.Build();

var repo = container.Resolve<IRepository>();
var fs = container.Resolve<FoodService>();

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