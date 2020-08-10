
namespace Challenge
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }

        public bool Equals(object o)
        {
            OrderItem other = (OrderItem) o;

            return other != null &&
                OrderId == other.OrderId &&
                ProductId == other.ProductId &&
                Quantity == other.Quantity;
        }
    }
}