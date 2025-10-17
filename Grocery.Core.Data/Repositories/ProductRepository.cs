using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using Microsoft.Data.Sqlite;

namespace Grocery.Core.Data.Repositories
{
    public class ProductRepository : DatabaseConnection, IProductRepository
    {
        private readonly List<Product> products = [];
        public ProductRepository()
        {
            List<Product> baseData = [
                new Product(1, "Melk", 300, new DateOnly(2025, 9, 25), 1.50m),
                new Product(2, "Kaas", 100, new DateOnly(2025, 9, 30), 5.00m),
                new Product(3, "Brood", 400, new DateOnly(2025, 9, 12), 1.99m),
                new Product(4, "Cornflakes", 0, new DateOnly(2025, 12, 31), 3.49m),
            ];

            List<SqliteParameter[]> insertInputParameters = [.. baseData.Select<Product, SqliteParameter[]>(item =>
                [
                    new SqliteParameter("Name", item.Name),
                    new SqliteParameter("Stock", item.Stock),
                    new SqliteParameter("ShelfLife", item.ShelfLife),
                    new SqliteParameter("Price", item.Price)
                ]
            )];

            //ISO 8601 format: date.ToString("o", CultureInfo.InvariantCulture)
            CreateTable(@"CREATE TABLE IF NOT EXISTS Product (
                            [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [Name] NVARCHAR(80) UNIQUE NOT NULL,
                            [Stock] INTEGER NOT NULL,
                            [ShelfLife] DATE NOT NULL,
                            [Price] DECIMAL(5, 2) NOT NULL)"); // range van 0 tot 999.99, dus 5 getallen en 2 achter de komma
            InsertMultipleWithTransactionAndParameters(
                "INSERT OR IGNORE INTO Product(Name, Stock, ShelfLife, Price) VALUES(@Name, @Stock, @ShelfLife, @Price)",
                insertInputParameters
            );
            GetAll();
        }

        public List<Product> GetAll()
        {
            products.Clear();
            string selectQuery = "SELECT Id, Name, Stock, date(ShelfLife), Price FROM Product;";
            OpenConnection();
            using (SqliteCommand command = new(selectQuery, Connection))
            {
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int stock = reader.GetInt32(2);
                    DateOnly shelfLife = DateOnly.FromDateTime(reader.GetDateTime(3));
                    decimal price = reader.GetDecimal(4);
                    products.Add(new(id, name, stock, shelfLife, price));
                }
            }
            CloseConnection();
            return products;
        }

        public Product? Get(int id)
        {
            string selectQuery = "SELECT Id, Name, Stock, date(ShelfLife), Price FROM Product WHERE Id = @Id;";
            Product? p = null;
            OpenConnection();
            using (SqliteCommand command = new(selectQuery, Connection))
            {
                command.Parameters.AddWithValue("Id", id);
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int Id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int stock = reader.GetInt32(2);
                    DateOnly shelfLife = DateOnly.FromDateTime(reader.GetDateTime(3));
                    decimal price = reader.GetDecimal(4);
                    p = new(Id, name, stock, shelfLife, price);
                }
            }
            CloseConnection();
            return p;
        }

        public Product Add(Product item)
        {
            string insertQuery = "INSERT INTO Product(Name, Stock, ShelfLife, Price) VALUES(@Name, @Stock, @ShelfLife, @Price) Returning RowId;";
            OpenConnection();
            using (SqliteCommand command = new(insertQuery, Connection))
            {
                command.Parameters.AddWithValue("Name", item.Name);
                command.Parameters.AddWithValue("Stock", item.Stock);
                command.Parameters.AddWithValue("ShelfLife", item.ShelfLife);
                command.Parameters.AddWithValue("Price", item.Price);

                item.Id = Convert.ToInt32(command.ExecuteScalar());
            }
            CloseConnection();
            return item;
        }

        public Product? Update(Product item)
        {
            int recordsAffected;
            string updateQuery = "UPDATE Product SET Name = @Name, Stock = @Stock, ShelfLife = @ShelfLife, Price = @Price WHERE Id = @Id;";
            OpenConnection();
            using (SqliteCommand command = new(updateQuery, Connection))
            {
                command.Parameters.AddWithValue("Name", item.Name);
                command.Parameters.AddWithValue("Stock", item.Stock);
                command.Parameters.AddWithValue("ShelfLife", item.ShelfLife);
                command.Parameters.AddWithValue("Price", item.Price);
                command.Parameters.AddWithValue("Id", item.Id);

                recordsAffected = command.ExecuteNonQuery();
            }
            CloseConnection();
            return item;
        }

        public Product? Delete(Product item)
        {
            string deleteQuery = "DELETE FROM Product WHERE Id = @Id;";
            OpenConnection();
            using (SqliteCommand command = new(deleteQuery, Connection))
            {
                command.Parameters.AddWithValue("Id", item.Id);
                command.ExecuteNonQuery();
            }
            CloseConnection();
            return item;
        }
    }
}
