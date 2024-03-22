namespace ppedv.CubesPizza.API.Model
{
    public class PizzaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public bool IsVeg { get; set; }
        public decimal Price { get; set; }
    }
}
