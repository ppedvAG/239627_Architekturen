using ppedv.CubesPizza.Core;
using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;
using System.ComponentModel;
using System.Windows.Input;

namespace ppedv.CubesPizza.UI.Wpf.ViewModels
{
    public class FoodAdminViewModel : INotifyPropertyChanged
    {
        IRepository repo;
        FoodService fs;
        private Pizza selectedPizza;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SaveCommand SaveCommand { get; set; }

        public ICollection<Pizza> PizzaList { get; set; }
        public Pizza SelectedPizza
        {
            get => selectedPizza; 
            set
            {
                selectedPizza = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedPizza"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PreiseOhneMwST"));
            }
        }

        public string PreiseOhneMwST
        {
            get
            {
                if (SelectedPizza == null)
                    return "---";

                return (SelectedPizza.Price * 2).ToString();
            }
        }

        public FoodAdminViewModel()
        {
            string conString = "Server=(localdb)\\mssqllocaldb;Database=CubesPizza_Tests;Trusted_Connection=true;";
            repo = new ppedv.CubesPizza.Data.EfCore.PizzaContextRepositoryAdapter(conString);
            fs = new FoodService(repo);

            PizzaList = new List<Pizza>(repo.GetAll<Pizza>());
            SaveCommand = new SaveCommand(repo);
        }


    }


    public class SaveCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        IRepository repo;

        public SaveCommand(IRepository repo)
        {
            this.repo = repo;
        }

        public void Execute(object? parameter)
        {
            repo.SaveAll();
        }
    }
}

