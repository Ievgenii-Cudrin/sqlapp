using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //private static string db_source = "kudrindb.database.windows.net";
        //private static string db_user = "kudrin";
        //private static string db_password = "Zhendel2008!";
        //private static string db_database = "kudrin-db";

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
        }

        public List<Product> GetProducts()
        {
            var c = _configuration;

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
