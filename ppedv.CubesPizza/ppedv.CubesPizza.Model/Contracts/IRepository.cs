namespace ppedv.CubesPizza.Model.Contracts
{
    public interface IRepository
    {
        T? GetById<T>(int id) where T : Entity;
        IEnumerable<T> GetAll<T>() where T : Entity;

        void Add<T>(Entity entity) where T : Entity;
        void Delete<T>(Entity entity) where T : Entity;
        void Update<T>(Entity entity) where T : Entity;

        void SaveAll();
    }
}
