
namespace Challenge
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Order Order { get; set; }

        public override bool Equals(object o)
        {
            Customer other = (Customer) o;

            return other != null &&
                Id == other.Id &&
                Name == other.Name;
        }
    }
}