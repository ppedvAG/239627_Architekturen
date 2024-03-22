namespace ppedv.CubesPizza.Model.Contracts
{
    public interface IRepository<T> where T : Entity
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();

        void Add(Entity entity);
        void Delete(Entity entity);
        void Update(Entity entity);
    }

    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetActiveOrderByStoredProc();
    }

    public interface IUnitOfWork
    {
        IRepository<Pizza> FoodRepository { get; }
        IRepository<Address> AddressRepository { get; }
        IOrderRepository OrderRepository { get; }

        void SaveAll();
    }
}
