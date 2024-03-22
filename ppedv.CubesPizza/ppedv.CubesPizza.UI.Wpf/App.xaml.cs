using Microsoft.Extensions.DependencyInjection;
using ppedv.CubesPizza.Core;
using ppedv.CubesPizza.Model.Contracts;
using ppedv.CubesPizza.UI.Wpf.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ppedv.CubesPizza.UI.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            string conString = "Server=(localdb)\\mssqllocaldb;Database=CubesPizza_Tests;Trusted_Connection=true;";

            services.AddSingleton<IRepository>(x=> new ppedv.CubesPizza.Data.EfCore.PizzaContextRepositoryAdapter(conString));
            services.AddScoped<FoodService>();
            services.AddSingleton<FoodAdminViewModel>();

            return services.BuildServiceProvider();
        }
    }

}
