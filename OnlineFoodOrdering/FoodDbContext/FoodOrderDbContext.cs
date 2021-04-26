using OnlineFoodOrdering.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineFoodOrdering.FoodDbContext
{
    public class FoodOrderDbContext:DbContext
    {
        public FoodOrderDbContext() : base("connString") { }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }

    }
}