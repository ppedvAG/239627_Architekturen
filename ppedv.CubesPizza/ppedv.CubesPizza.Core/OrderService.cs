using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;

namespace ppedv.CubesPizza.Core
{
    public class OrderService : IOrderService
    {
        IUnitOfWork repo;
        IFoodService foodService;
        IOvenControl ovenControl;

        public OrderService(IUnitOfWork repo, IFoodService foodService, IOvenControl ovenControl)
        {
            this.repo = repo;
            this.foodService = foodService;
            this.ovenControl = ovenControl;
        }

        public void PlaceOrder(Order order)
        {
            var speisen = foodService.GetSpeisekarte();
            //todo check speisen from oder

            ovenControl.StartOven(250);

            order.OrderDate = DateTime.Now;

            repo.OrderRepository.Add(order);
            repo.SaveAll();
        }
    }
}
