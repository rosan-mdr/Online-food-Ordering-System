using OnlineFoodOrdering.FoodDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFoodOrdering.Controllers
{
    [AllowAnonymous]
    public class HomeController : FrontController
    {
       
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

    }
}