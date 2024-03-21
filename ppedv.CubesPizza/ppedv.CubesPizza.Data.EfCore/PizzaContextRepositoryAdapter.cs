using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;

namespace ppedv.CubesPizza.Data.EfCore
{
    public class PizzaContextRepositoryAdapter : IRepository
    {
        private PizzaContext pizzaContext;

        public PizzaContextRepositoryAdapter(string conString)
        {
           pizzaContext = new PizzaContext(conString);
        }

        public void Add<T>(Entity entity) where T : Entity
        {
            pizzaContext.Add(entity);
        }

        public void Delete<T>(Entity entity) where T : Entity
        {
            pizzaContext.Remove(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return pizzaContext.Set<T>().ToList();
        }

        public T? GetById<T>(int id) where T : Entity
        {
            return pizzaContext.Find<T>(id);
        }

        public void SaveAll()
        {
            pizzaContext.SaveChanges();
        }

        public void Update<T>(Entity entity) where T : Entity
        {
            pizzaContext.Remove(entity);
        }
    }
}
