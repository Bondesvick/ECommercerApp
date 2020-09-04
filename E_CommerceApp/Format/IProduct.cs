using System;

namespace E_CommerceApp.Format
{
    public interface IProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public Decimal CostPrice { get; set; }
        public DateTime DateAdded { get; set; }
    }
}