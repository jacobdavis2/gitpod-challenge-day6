using System.Collections.Generic;

namespace Challenge
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer {get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        public override bool Equals(object o)
        {
            Order other = (Order) o;

            return other != null &&
                Id == other.Id &&
                CustomerId == other.CustomerId;
        }
    }
}