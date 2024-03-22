using Microsoft.Extensions.DependencyInjection;
using ppedv.CubesPizza.UI.Wpf.ViewModels;
using System.Windows.Controls;

namespace ppedv.CubesPizza.UI.Wpf.Views
{
    /// <summary>
    /// Interaction logic for FoodAdminView.xaml
    /// </summary>
    public partial class FoodAdminView : UserControl
    {
        public FoodAdminView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<FoodAdminViewModel>();
        }
    }
}
