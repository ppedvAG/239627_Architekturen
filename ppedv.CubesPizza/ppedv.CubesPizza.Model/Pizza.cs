namespace ppedv.CubesPizza.Model
{
    public class Pizza : Entity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsVegetarian { get; set; } = true;
        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

    }
}
