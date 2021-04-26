using OnlineFoodOrdering.FoodDbContext;
using OnlineFoodOrdering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFoodOrdering.Controllers
{
    public class FrontController : Controller
    {
        protected FoodOrderDbContext db = new FoodOrderDbContext();
    

        public List<Cart> CartItems()
        {
            List<Cart> items = null;
            if (Session["cart"] == null)
            {
                items = new List<Cart>();
                Session["cart"] = items;
            }
            else {
                items = (List<Cart>)Session["cart"];
            }
            return items;
        }

        public void RemoveSession() {
            Session.Remove("cart");
        }
    }
}