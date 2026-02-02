using Microsoft.Data.Sqlite;
using PraktikaApp.Models;
using System.Collections.Generic;

namespace PraktikaApp.Data
{
    public class OrderService
    {
        private readonly string _connectionString = "Data Source=orders.db";

        // Метод для получения всех заказов (аналог GetAllProducts)
        public List<Order> GetAllOrders()
        {
            var orders = new List<Order>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Orders";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            Id = reader.GetInt32(0),
                            CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                            CustomerPhone = reader.GetString(reader.GetOrdinal("CustomerPhone")),
                            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        });
                    }
                }
            }

            return orders;
        }
    }
}
