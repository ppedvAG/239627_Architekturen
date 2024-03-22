using ppedv.CubesPizza.Core;
using ppedv.CubesPizza.Data.EfCore;
using ppedv.CubesPizza.Model.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string conString = "Server=(localdb)\\mssqllocaldb;Database=CubesPizza_Tests;Trusted_Connection=true;";
builder.Services.AddScoped<IUnitOfWork>(x => new PizzaContextUnitOfWorkAdapter(conString));
builder.Services.AddScoped<FoodService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
