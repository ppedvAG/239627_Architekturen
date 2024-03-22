namespace ppedv.CubesPizza.Model
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public virtual Address BillingAddress { get; set; } = new Address();
        public virtual Address? DeliveryAddress { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
