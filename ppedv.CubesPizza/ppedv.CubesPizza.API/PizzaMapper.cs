using ppedv.CubesPizza.API.Model;
using ppedv.CubesPizza.Model;

namespace ppedv.CubesPizza.API
{
    public static class PizzaMapper
    {
        // Von Pizza zu PizzaDTO
        public static PizzaDTO MapToDTO(Pizza pizza)
        {
            return new PizzaDTO
            {
                Id = pizza.Id,
                Name = pizza.Name,
                IsVeg = pizza.IsVegetarian,
                Price = pizza.Price
            };
        }

        // Von PizzaDTO zu Pizza
        public static Pizza MapToPizza(PizzaDTO pizzaDTO)
        {
            return new Pizza
            {
                // Id wird üblicherweise beim Erstellen einer neuen Entität vom Datenbank-System generiert,
                // daher setzen wir es hier nicht explizit, es sei denn, es wird für ein Update verwendet.
                Name = pizzaDTO.Name,
                IsVegetarian = pizzaDTO.IsVeg,
                Price = pizzaDTO.Price
            };
        }
    }
}
