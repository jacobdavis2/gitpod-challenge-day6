using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge
{
    class Program
    {
        static async Task SeedDatabaseAsync(ChallengeContext context)
        {
            // Customers
            await context.Customers.AddAsync(new Customer
            {
                Id = 1,
                Name = "Billy Bob"
            });
            await context.Customers.AddAsync(new Customer
            {
                Id = 2,
                Name = "Mary Sue"
            });
            await context.Customers.AddAsync(new Customer
            {
                Id = 3,
                Name = "Helen MacMillan"
            });
            await context.Customers.AddAsync(new Customer
            {
                Id = 4,
                Name = "Smalls"
            });
            await context.Customers.AddAsync(new Customer
            {
                Id = 5,
                Name = "Marcus"
            });
            await context.Customers.AddAsync(new Customer
            {
                Id = 6,
                Name = "Enrietta"
            });

            // Products
            await context.Products.AddAsync(new Product
            {
                Id = 1,
                Name = "Milk",
                Price = 3.99
            });
            await context.Products.AddAsync(new Product
            {
                Id = 2,
                Name = "Eggs",
                Price = 5.49
            });
            await context.Products.AddAsync(new Product
            {
                Id = 3,
                Name = "Sugar",
                Price = 2.99
            });
            await context.Products.AddAsync(new Product
            {
                Id = 4,
                Name = "Avocados",
                Price = 17.99
            });
            await context.Products.AddAsync(new Product
            {
                Id = 5,
                Name = "Peas",
                Price = .38
            });
            await context.Products.AddAsync(new Product
            {
                Id = 6,
                Name = "Eye of Newt",
                Price = 6.66
            });

            // Orders
            await context.Orders.AddAsync(new Order
            {
                Id = 1,
                CustomerId = 3
            });
            await context.Orders.AddAsync(new Order
            {
                Id = 2,
                CustomerId = 5
            });

            // OrderItems
            await context.OrderItems.AddAsync(new OrderItem 
            {
                OrderId = 1,
                ProductId = 3,
                Quantity = 2
            });
            await context.OrderItems.AddAsync(new OrderItem 
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 20
            });
            await context.OrderItems.AddAsync(new OrderItem 
            {
                OrderId = 2,
                ProductId = 5,
                Quantity = 5
            });

            await context.SaveChangesAsync();
        }

        public static bool ListsEqual<T>(List<T> subject, List<T> other)
        {
            if (subject.Count != other.Count)
                return false;

            for (int i = 0; i < subject.Count; ++i)
            {
                if (!subject[i].Equals(other[i]))
                    return false;
            }

            return true;
        }

        static async Task Main(string[] args)
        {
            ChallengeContext _context;

            using (DbConnection _connection = new SqliteConnection("Filename=:memory:"))
            {
                _connection.Open();

                _context = new ChallengeContext(new DbContextOptionsBuilder()
                    .UseSqlite(_connection)
                    .Options);
                RelationalDatabaseCreator databaseCreator = 
                    (RelationalDatabaseCreator) _context.Database.GetService<IDatabaseCreator>();
                databaseCreator.CreateTables();

                await SeedDatabaseAsync(_context);

                // Basic database tests - if they fail, the context doesn't work
                Console.WriteLine("Testing veracity of database relationshps...");
                try
                {
                    Console.WriteLine("Testing Customers.");
                    List<Customer> customers = await _context.Customers.ToListAsync();
                    foreach (Customer c in customers)
                    {
                        Console.WriteLine("  Name: " + c.Name);
                    }

                    Console.WriteLine("Testing Orders.");
                    List<Order> orders = await _context.Orders
                        .Include(o => o.OrderItems)
                        .ToListAsync();
                    foreach (Order o in orders)
                    {
                        Console.WriteLine("Order Id:" + o.Id);
                        foreach (OrderItem oi in o.OrderItems)
                        {
                            Product p = await _context.Products.FindAsync(oi.ProductId);
                            Console.WriteLine("  " + p.Name + " x" + oi.Quantity);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                    Console.WriteLine("Stack Trace: " + e.StackTrace);
                    Console.WriteLine("Your context may be malformed.");
                }

                // Section for assigned functions
                Console.WriteLine("Testing assigned functions...");

                List<Product> test1 = await Queries.AllProductsBeginningWithE(_context), answer1 = new List<Product> 
                { 
                    new Product
                    {
                        Id = 2,
                        Name = "Eggs",
                        Price = 5.49
                    },
                    new Product
                    {
                        Id = 6,
                        Name = "Eye of Newt",
                        Price = 6.66
                    }
                };
                if (ListsEqual(test1, answer1))
                    Console.WriteLine("Test 1: Passed!");
                else
                    Console.WriteLine("Test 1: Failed.");

                List<double> test2 = await Queries.TotalPricesOfAllOrders(_context), answer2 = new List<double>
                {
                    85.78,
                    1.9
                };
                if (ListsEqual(test2, answer2))
                    Console.WriteLine("Test 2: Passed!");
                else
                    Console.WriteLine("Test 2: Failed.");

                List<Customer> test3 = await Queries.AllCustomersWithOrders(_context), answer3 = new List<Customer>
                {
                    new Customer
                    {
                        Id = 3,
                        Name = "Helen MacMillan"
                    },
                    new Customer
                    {
                        Id = 5,
                        Name = "Marcus"
                    }
                };
                if (ListsEqual(test3, answer3))
                    Console.WriteLine("Test 3: Passed!");
                else
                    Console.WriteLine("Test 3: Failed.");

                List<Order> test4 = await Queries.AllOrdersAboveTwentyDollars(_context), answer4 = new List<Order>
                {
                    new Order
                    {
                        Id = 1,
                        CustomerId = 3
                    }
                };
                if (ListsEqual(test4, answer4))
                    Console.WriteLine("Test 4: Passed!");
                else
                    Console.WriteLine("Test 4: Failed.");
            }
        }
    }
}
