using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;

namespace ppedv.CubesPizza.Core
{
    public class FoodService : IFoodService
    {
        private IRepository repository;
        public FoodService(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Pizza> GetSpeisekarte(bool nurVegetarisch = false)
        {
            //todo: mehr logic
            var query = repository.GetAll<Pizza>();

            if (nurVegetarisch)
                query = query.Where(x => x.IsVegetarian == true);

            return query.OrderBy(x => x.Price);
        }

    }
}
