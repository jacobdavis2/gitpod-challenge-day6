using System.Collections.Generic;

namespace Challenge
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price {get; set; }


        public ICollection<OrderItem> OrderItems { get; set; }

        public override bool Equals(object o)
        {
            Product other = (Product) o;
            
            return other != null &&
                Id == other.Id &&
                Name == other.Name &&
                Price == other.Price;
        }
    }
}