using OnlineFoodOrdering.AprioriAlgorithm;
using OnlineFoodOrdering.AprioriAlgorithm.Model;
using OnlineFoodOrdering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;

namespace OnlineFoodOrdering.Controllers
{
    public class CartController : FrontController
    {
      
        // GET: Cart
        // FoodOrderDbContext dbContext = new FoodOrderDbContext();
        public ActionResult Cart()
        {
            AprioriAlgo algo = new AprioriAlgo();
            Output output = new Output();
            List<string> itemsList = db.Products.Select(model => model.ProductCode).ToList();
            List<Cart> carts = new List<Models.Cart>();
            carts = db.Carts.ToList();
            int transactionId = 1;
            List<string> transaction = new List<string>();
            string tran = "";
            foreach (Cart cart in carts) {
                if (transactionId == cart.TransactionNo)
                {
                    tran += cart.ProductId + " ";
                }
                else {
                    transaction.Add(tran);
                    transactionId++;
                    tran = "";
                    tran += cart.ProductId + " ";
                }
            }
            
           output = algo.ProcessTransaction(0.4,0.5, itemsList,transaction.ToArray());
           User user = db.Users.Where(model=>model.Email==User.Identity.Name).FirstOrDefault();
            List<Product> recommendation = new List<Product>();
            string prevItem = "";
            ViewBag.Recommendation = "";
            foreach (Cart cart in carts) {
                if (cart.UserId==user.Id) {
                    Product product = db.Products.Where(model => model.Id == cart.ProductId).FirstOrDefault();
                    if (output.StrongRules.Where(model => model.X == product.ProductCode).Count() > 0) {
                        Rule item = output.StrongRules.Where(model => model.X == product.ProductCode).FirstOrDefault();
                        int id = Convert.ToInt32(item.Y);
                        string name = db.Products.Find(id).Name;
                        if (!prevItem.Contains(name)) {
                            prevItem += name;
                            recommendation.Add(db.Products.Where(model => model.Name == name).FirstOrDefault());
                        }
                        
                    }
                }
            }
            ViewBag.Recommendation = recommendation;
           
            return View(CartItems());
        }

        [HttpPost]
        public ActionResult AddToCart(Cart item) {
            List<Cart> items = CartItems();
            item.Product = db.Products.Find(item.ProductId);
            bool exist = false;
            foreach (Cart c in items) {
                if (c.ProductId == item.ProductId)
                {
                    ++c.Quantity;
                    exist = true;
                    break;
                }
            }
            if (!exist) {
                items.Add(item);
            }
           
            return Redirect("~/Home/Index#menu");
        }

        public ActionResult Delete(int id) {
            List<Cart> items = CartItems();
            foreach (Cart c in items) {
                if (c.ProductId == id) {
                    items.Remove(c);
                    break;
                }
            }
            return RedirectToAction("Cart");
        }

        public ActionResult Edit(int id) {
            return PartialView();
        }

        public ActionResult CheckOut() {
            
            List<Cart> items = CartItems();
            if (items==null) {
                return RedirectToAction("Error");
            }
            int tNo = Convert.ToInt32(db.Carts.DefaultIfEmpty().Max(model => (int?)model.TransactionNo));
            int transactionNo = tNo + 1;
            User user = new User();
            string email = User.Identity.Name;
            user = db.Users.Where(model => model.Email == email).FirstOrDefault();
            int totalQuantity = items.Sum(model=>model.Quantity);
            decimal total = items.Sum(model => model.Price);
            foreach (Cart item in items)
            {
                item.Product = db.Products.Find(item.ProductId);
                item.ProductId = item.Product.Id;
                item.UserId = user.Id;
                item.TransactionNo = transactionNo;
                db.Carts.Add(item);
                db.SaveChanges();
            }
            RemoveSession();
            MailMessage mm = new MailMessage("sandhyal360@gmail.com", email);
            string mail = string.Format("Dear {0}," +
                        "<br>Thank you for purchasing from us. Your order will be delivered shortly in your"
                        + "address {1}"
                        , user.FirstName, user.Address);
            mm.Body = mail;
            mm.IsBodyHtml = true;
            mm.Subject = "Purchase Successfull";
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 25;
            smtp.Host = "smtp.wlink.com.np";
            smtp.Send(mm);
            return RedirectToAction("Success");
        }

        public ActionResult Success() {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}