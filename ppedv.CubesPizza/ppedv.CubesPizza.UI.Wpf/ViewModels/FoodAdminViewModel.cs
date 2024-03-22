using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ppedv.CubesPizza.Core;
using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ppedv.CubesPizza.UI.Wpf.ViewModels
{
    public partial class FoodAdminViewModel : ObservableObject
    {
        IRepository repo;
        FoodService fs;
        public SaveCommand SaveCommand { get; set; }

        [RelayCommand]
        private void AddNewPizza()
        {
            var p = new Pizza() { Name = "NEU", Price = 6.66m };
            repo.Add<Pizza>(p);
            PizzaList.Add(p);
        }

        public ObservableCollection<Pizza> PizzaList { get; set; }

        //private Pizza selectedPizza;
        //public Pizza SelectedPizza
        //{
        //    get => selectedPizza; 
        //    set
        //    {
        //        selectedPizza = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedPizza"));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PreiseOhneMwST"));
        //    }
        //}

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PreiseOhneMwST))]
        private Pizza selectedPizza;

        public string PreiseOhneMwST
        {
            get
            {
                if (SelectedPizza == null)
                    return "---";

                return (SelectedPizza.Price * 2).ToString();
            }
        }

        public FoodAdminViewModel(IRepository repo, FoodService fs)
        {
            this.repo = repo;
            this.fs = fs;
            PizzaList = new ObservableCollection<Pizza>(repo.GetAll<Pizza>());
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

