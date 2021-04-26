using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrdering.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public int TransactionNo { get; set; }
    }
}