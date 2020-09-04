using E_CommerceApp.Format;
using System.Collections.Generic;

namespace E_CommerceApp
{
    public interface IDataOperations
    {
        public List<Product> ProductData { get; set; }

        public List<Cart> CartData { get; set; }

        public List<string> CategoryList { get; set; }

        /// <summary>
        /// Reads the database and populates the dataList
        /// </summary>
        public void LoadProductData();

        /// <summary>
        /// Adds new data into the database
        /// </summary>
        /// <param name="productName">ProductName from database to add</param>
        /// <param name="categoryId"></param>
        /// <param name="costPrice">CostPrice from database to add</param>
        public void AddProductData(string productName, int categoryId, decimal costPrice);

        /// <summary>
        /// A methods that handles updating operations for the database
        /// </summary>
        /// <param name="id">the id of the row to update</param>
        /// <param name="productName">Product Name from database column</param>
        /// <param name="categoryId"></param>
        /// <param name="costPrice">CostPrice Name from database column</param>
        public void UpdateProductData(int id, string productName, int categoryId, decimal costPrice);

        /// <summary>
        /// Deletes a record in the database at a particular id
        /// </summary>
        /// <param name="id">The id of the row to delete</param>
        public void DeleteProductData(int id);

        /// <summary>
        /// Filters data by Product name
        /// </summary>
        /// <param name="name">Product name to search</param>
        public void SearchProductByName(string name);

        /// <summary>
        /// Filters data by Price
        /// </summary>
        /// <param name="price">price range to filter</param>
        public void SearchProductByPrice(decimal price);

        /// <summary>
        /// Reads data from the cart table
        /// </summary>
        public void LoadCart();

        /// <summary>
        /// Adds a product to Cart
        /// </summary>
        /// <param name="productId">Id of product to add</param>
        /// <param name="categoryId"></param>
        /// <param name="quantity">Quantity of product</param>
        public void AddToCart(int productId, int categoryId, int quantity);

        /// <summary>
        /// Updates item in cart
        /// </summary>
        /// <param name="id">Id of item to update</param>
        /// <param name="quantity">New quantity to update</param>
        public void UpdateCart(int id, int quantity);

        /// <summary>
        /// Deletes item from Cart
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void DeleteFromCart(int id);

        /// <summary>
        /// Loads the product categories into List
        /// </summary>
        public void LoadCategoryData();
    }
}