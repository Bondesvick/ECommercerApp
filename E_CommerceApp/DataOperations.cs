using E_CommerceApp.Format;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace E_CommerceApp
{
    public class DataOperations : IDataOperations
    {
        public List<Product> ProductData { get; set; }
        public List<Cart> CartData { get; set; }
        public List<string> CategoryList { get; set; }

        /// <summary>
        /// Database connection string
        /// </summary>
        public SqlConnection Conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\hp\\OneDrive\\Desktop\\week-7-task-Bondesvick\\E_CommerceApp\\Data\\E_CommerceData.mdf;Integrated Security=True");

        /// <summary>
        /// Adds new data into the database
        /// </summary>
        /// <param name="productName">ProductName from database to add</param>
        /// <param name="categoryId"></param>
        /// <param name="costPrice">CostPrice from database to add</param>
        public void AddProductData(string productName, int categoryId, decimal costPrice)
        {
            Conn.Open();
            SqlCommand command = Conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO [dbo].[Product] (ProductName,CategoryId,CostPrice) VALUES ('" + productName + "', '" + categoryId + "', '" + costPrice + "')";
            command.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Deletes a record in the database at a particular id
        /// </summary>
        /// <param name="id">The id of the row to delete</param>
        public void DeleteProductData(int id)
        {
            Conn.Open();

            SqlCommand command = Conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM [dbo].[Product] WHERE ProductId = '" + id + "'";
            command.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Reads the database and populates the dataList
        /// </summary>
        public void LoadProductData()
        {
            List<Product> theData = new List<Product>();
            Conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Product]", Conn);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    theData.Add(new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        CategoryId = reader.GetInt32(2),
                        CostPrice = reader.GetDecimal(3),
                        DateAdded = reader.GetDateTime(4)
                    });
                }

                //Dt = new DataTable();
                //Dt.Load(reader);
                ProductData = theData;
            }

            reader.Close();
            Conn.Close();
        }

        /// <summary>
        /// A methods that handles updating operations for the database
        /// </summary>
        /// <param name="id">the id of the row to update</param>
        /// <param name="productName">Product Name from database column</param>
        /// <param name="categoryId"></param>
        /// <param name="costPrice">CostPrice Name from database column</param>
        public void UpdateProductData(int id, string productName, int categoryId, decimal costPrice)
        {
            Conn.Open();

            SqlCommand command = Conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE [dbo].[Product] SET ProductName = '" + productName + "', CategoryId = '" + categoryId + "', CostPrice = '" + costPrice + "' WHERE ProductId = '" + id + "'";
            command.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Filters data by Product name
        /// </summary>
        /// <param name="name">Product name to search</param>
        public void SearchProductByName(string name)
        {
            List<Product> theData = new List<Product>();
            Conn.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Product] WHERE ProductName LIKE '%" + name + "%'", Conn);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    theData.Add(new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        CategoryId = reader.GetInt32(2),
                        CostPrice = reader.GetDecimal(3),
                        DateAdded = reader.GetDateTime(4)
                    });
                }
                ProductData = theData;
            }

            reader.Close();
            Conn.Close();
        }

        /// <summary>
        /// Filters data by price
        /// </summary>
        /// <param name="price">price range to filter</param>
        public void SearchProductByPrice(decimal price)
        {
            List<Product> theData = new List<Product>();
            Conn.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Product] WHERE CostPrice <= '" + price + "'", Conn);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    theData.Add(new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        CategoryId = reader.GetInt32(2),
                        CostPrice = reader.GetDecimal(3),
                        DateAdded = reader.GetDateTime(4)
                    });
                }
                ProductData = theData;
            }

            reader.Close();
            Conn.Close();
        }

        /// <summary>
        /// Reads data from the cart table
        /// </summary>
        public void LoadCart()
        {
            List<Cart> cartData = new List<Cart>();
            Conn.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Cart]", Conn);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cartData.Add(new Cart()
                    {
                        Id = reader.GetInt32(0),
                        ProductId = reader.GetInt32(1),
                        CategoryId = reader.GetInt32(2),
                        Quantity = reader.GetInt32(3),
                        DateOfOrder = reader.GetDateTime(4)
                    });
                }
                CartData = cartData;
            }

            reader.Close();
            Conn.Close();
        }

        /// <summary>
        /// Adds a product to Cart
        /// </summary>
        /// <param name="productId">Id of product to add</param>
        /// <param name="categoryId"></param>
        /// <param name="quantity">Quantity of product</param>
        public void AddToCart(int productId, int categoryId, int quantity)
        {
            Conn.Open();

            SqlCommand command = Conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO [dbo].[Cart] (ProductId,CategoryId,Quantity) VALUES ('" + productId + "','" + categoryId + "', '" + quantity + "')";
            command.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Updates item in cart
        /// </summary>
        /// <param name="id">Id of item to update</param>
        /// <param name="quantity">New quantity to update</param>
        public void UpdateCart(int id, int quantity)
        {
            Conn.Open();

            SqlCommand command = Conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE [dbo].[Cart] SET Quantity = '" + quantity + "' WHERE Id = '" + id + "'";
            command.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Deletes item from Cart
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void DeleteFromCart(int id)
        {
            Conn.Open();

            SqlCommand command = Conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM [dbo].[Cart] WHERE Id = '" + id + "'";
            command.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Loads the product categories into List
        /// </summary>
        public void LoadCategoryData()
        {
            List<string> theData = new List<string>();
            Conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[ProductCategory]", Conn);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    theData.Add(reader.GetString(1));
                }
                CategoryList = theData;
            }

            reader.Close();
            Conn.Close();
        }
    }
}