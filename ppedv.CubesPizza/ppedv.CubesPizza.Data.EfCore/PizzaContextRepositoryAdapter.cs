using Microsoft.EntityFrameworkCore;
using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;

namespace ppedv.CubesPizza.Data.EfCore
{
    public class PizzaContextUnitOfWorkAdapter : IUnitOfWork
    {
        public IRepository<Pizza> FoodRepository => new PizzaContextRepositoryAdapter<Pizza>(pizzaContext);

        public IRepository<Address> AddressRepository => new PizzaContextRepositoryAdapter<Address>(pizzaContext);

        public IOrderRepository OrderRepository => new PizzaContextOrderRepositoryAdapter(pizzaContext);

        public void SaveAll()
        {
            pizzaContext.SaveChanges();
        }

        private PizzaContext pizzaContext;

        public PizzaContextUnitOfWorkAdapter(string conString)
        {
            pizzaContext = new PizzaContext(conString);
        }
    }

    public class PizzaContextOrderRepositoryAdapter : PizzaContextRepositoryAdapter<Order>, IOrderRepository
    {
        public PizzaContextOrderRepositoryAdapter(PizzaContext pizzaContext) : base(pizzaContext)
        { }

        public IEnumerable<Order> GetActiveOrderByStoredProc()
        {
            pizzaContext.Database.ExecuteSql($"SELECT * FROM master;");
            return null;
        }
    }

    public class PizzaContextRepositoryAdapter<T> : IRepository<T> where T : Entity
    {
        protected PizzaContext pizzaContext;

        public PizzaContextRepositoryAdapter(PizzaContext pizzaContext)
        {
            this.pizzaContext = pizzaContext;
        }

        public void Add(Entity entity)
        {
            pizzaContext.Add(entity);
        }

        public void Delete(Entity entity)
        {
            pizzaContext.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return pizzaContext.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return pizzaContext.Find<T>(id);
        }


        public void Update(Entity entity)
        {
            pizzaContext.Update(entity);
        }
    }
}
