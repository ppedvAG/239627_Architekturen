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
        IUnitOfWork uow;
        FoodService fs;
        public SaveCommand SaveCommand { get; set; }

        [RelayCommand]
        private void AddNewPizza()
        {
            var p = new Pizza() { Name = "NEU", Price = 6.66m };
            uow.FoodRepository.Add(p);
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

        public FoodAdminViewModel(IUnitOfWork uow, FoodService fs)
        {
            this.uow = uow;
            this.fs = fs;
            PizzaList = new ObservableCollection<Pizza>(uow.FoodRepository.GetAll());
            SaveCommand = new SaveCommand(uow);
        }


    }


    public class SaveCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        IUnitOfWork repo;

        public SaveCommand(IUnitOfWork repo)
        {
            this.repo = repo;
        }

        public void Execute(object? parameter)
        {
            repo.SaveAll();
        }
    }
}

