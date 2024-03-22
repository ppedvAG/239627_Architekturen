using Autofac;
using Autofac.Core;
using ppedv.CubesPizza.Core;
using ppedv.CubesPizza.Devices.BinFord;
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
builder.RegisterType<FoodService>().As<IFoodService>();
builder.RegisterType<OrderService>().As<IOrderService>();
builder.RegisterType<BinFordOvenControl>().As<IOvenControl>();
builder.Register(x => new ppedv.CubesPizza.Data.EfCore.PizzaContextUnitOfWorkAdapter(conString)).As<IUnitOfWork>().SingleInstance();
var container = builder.Build();

var uow = container.Resolve<IUnitOfWork>();
var fs = container.Resolve<IFoodService>();
var speisen = fs.GetSpeisekarte();
var os = container.Resolve<IOrderService>();

var testOrder = new Order();
testOrder.BillingAddress = new Address() { Line1 = "Fred Feuerstein", Street = "Steinstr. ", City = "?" };
testOrder.Items.Add(new OrderItem() { Amount = 2, ItemPrice = 12.40m, Pizza = speisen.ElementAt(3) });
os.PlaceOrder(testOrder);

Console.WriteLine("Speisekarte");
foreach (var p in speisen)
{
    Console.WriteLine($"{p.Name} {p.Price:c} {(p.IsVegetarian ? "🥦" : "🍖")}");
}


Console.WriteLine("Bestellungen");
foreach (var o in uow.OrderRepository.GetAll())
{
    Console.WriteLine($"{o.OrderDate:g} {o.CustomerName}");
}