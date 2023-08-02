using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "kudrindb.database.windows.net";
        private static string db_user = "kudrin";
        private static string db_password = "Zhendel2008!";
        private static string db_database = "kudrin-db";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;

            return new SqlConnection( _builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            var conn = GetConnection();
            var products = new List<Product>();
            var statement = "Select ProductId,ProductName, Quantity from Products";
            conn.Open();
            var cmd = new SqlCommand(statement, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    };

                    products.Add(product);
                }
            }

            conn.Close();

            return products;
        }

    }
}
