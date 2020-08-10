using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

/*
    Complete the following four functions such that all tests pass.
    The function descriptions are as follows:
        1. AllProductsBeginningWithE: Return a list of Products whose name starts with 'E'.
        2. TotalPricesOfAllOrders: Return a list of doubles in the order of the list of Orders,
            where each double is the total price of the respective order.
        3. AllCustomersWithOrders: Return a list of the Customers who have an Order.
        4. AllOrdersAboveTwentyDollars: Return all orders whose total is above 20 dollars (i.e., total price > 20)
*/

namespace Challenge
{
    public class Queries
    {
        public static async Task<List<Product>> AllProductsBeginningWithE(ChallengeContext context)
        {
            return new List<Product>();
        }
        public static async Task<List<double>> TotalPricesOfAllOrders(ChallengeContext context)
        {
            return new List<double>();
        }
        public static async Task<List<Customer>> AllCustomersWithOrders(ChallengeContext context)
        {
            return new List<Customer>();
        }

        public static async Task<List<Order>> AllOrdersAboveTwentyDollars(ChallengeContext context)
        {
            return new List<Order>();
        }
    }
}