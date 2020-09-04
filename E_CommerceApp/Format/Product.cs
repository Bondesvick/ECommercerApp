using System;

namespace E_CommerceApp.Format
{
    public class Product : IProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal CostPrice { get; set; }
        public DateTime DateAdded { get; set; }
    }
}