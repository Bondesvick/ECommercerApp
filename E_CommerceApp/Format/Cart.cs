using System;

namespace E_CommerceApp.Format
{
    public class Cart : ICart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfOrder { get; set; }
    }
}