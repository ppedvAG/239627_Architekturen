﻿using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;

namespace ppedv.CubesPizza.Core
{
    public class FoodService : IFoodService
    {
        private IUnitOfWork uow;
        public FoodService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IEnumerable<Pizza> GetSpeisekarte(bool nurVegetarisch = false)
        {
            //todo: mehr logic
            var query = uow.FoodRepository.GetAll();

            if (nurVegetarisch)
                query = query.Where(x => x.IsVegetarian == true);

            return query.OrderBy(x => x.Price);
        }

    }
}
