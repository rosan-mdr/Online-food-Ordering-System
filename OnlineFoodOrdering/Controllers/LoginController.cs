using OnlineFoodOrdering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineFoodOrdering.Controllers
{
    [AllowAnonymous]
    public class LoginController : FrontController
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel lvm) {
            if (ModelState.IsValid) {
                User user = db.Users.Where(model => model.Email == lvm.Email&&model.Password==lvm.Password).Where(model => model.Status == true).FirstOrDefault();
                if (user == null)
                {
                    ViewBag.ErrorMessage = "Wrong Email/Password";
                }
                else {
                    FormsAuthentication.SetAuthCookie(lvm.Email,false);
                    if (user.AdminRole == true) {
                        return Redirect("~/Admin/Product/Index");
                    }
                    return RedirectToAction("Cart","Cart");
                }
                //check for user
            }
            return View("Login");
        }

        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                 User user = new User();
                 
                if (db.Users.Where(model => model.Email == rvm.Email).FirstOrDefault() != null)
                {
                    ViewBag.ErrorMessage = "Email already exists";
                }
                else {
                    
                    user.FirstName = rvm.FirstName;
                    user.LastName = rvm.LastName;
                    user.Email = rvm.Email;
                    user.Phone = rvm.Phone;
                    user.Address = rvm.Address;
                    user.Password = rvm.Password;
                    user.AdminRole = false;
                    user.Status = true;
                    user.AddedDate = DateTime.Now;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
            }
            return View("Register");
        }

        public ActionResult ForgotPassword() {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email) {
            User user = db.Users.Where(model => model.Email == email).First();
            ViewBag.ErrorMEssage = "";
            if (user!=null) {
                MailMessage mm = new MailMessage("sandhyal360@gmail.com", email);
                string mail = string.Format("Dear {0}"+
                            "<br>Please click on this link"
                            +"to reset your password <a href=\"{1}\">Forgot Password</a>"
                            ,user.FirstName,Url.Action("ResetPassword","Login",new { Email = email},Request.Url.Scheme));
                mm.Body = mail;
                mm.IsBodyHtml = true;
                mm.Subject = "Reset Password";
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 25;
                smtp.Host = "smtp.wlink.com.np";
                smtp.Send(mm);
                return RedirectToAction("Index","Home");
            }
            ViewBag.ErrorMessage = "Your Id does not Exists";
            return View() ;
        }

        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        public ActionResult ResetPassword(string email) {
            if (email == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = new Models.User();
            user = db.Users.Where(model => model.Email == email).FirstOrDefault();
            RegistrationViewModel rvm = new RegistrationViewModel();
            rvm.FirstName = user.FirstName;
            rvm.LastName = user.LastName;
            rvm.Email = user.Email;
            rvm.Phone = user.Phone;
            rvm.Address = user.Address;
            rvm.Password = user.Password;

            return View(rvm);
        }

        [HttpPost]
        public ActionResult ResetPassword(RegistrationViewModel rvm) {
            if (ModelState.IsValid) {
                User user = db.Users.Where(model => model.Email == rvm.Email).FirstOrDefault();
                user.Password = rvm.Password;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}