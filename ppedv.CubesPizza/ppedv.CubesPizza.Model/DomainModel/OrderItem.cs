namespace ppedv.CubesPizza.Model
{
    public class OrderItem : Entity
    {
        public virtual Order? Order { get; set; }
        public virtual Pizza? Pizza { get; set; }
        public int Amount { get; set; } = 1;
        public decimal ItemPrice { get; set; }
    }
}
