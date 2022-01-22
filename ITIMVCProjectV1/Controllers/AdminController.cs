using ITIMVCProjectV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIMVCProjectV1.ViewModel;
using System.Data.Entity;

namespace ITIMVCProjectV1.Controllers
{
    public class AdminController : Controller
    {
        DB con = new DB();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["AdminUserName"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.isvalid = true;
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminLogin admin)
        {
            if(! ModelState.IsValid)
            {
                return View();

            }
            var AdminDB = con.Admins.SingleOrDefault(a => a.ID == 1);
            if (AdminDB.UserName.Trim() != admin.UserName || AdminDB.password.Trim() != admin.Password)
            {
                ViewBag.isvalid = false;
                return View();

            }
            Session["AdminUserName"] = AdminDB.UserName.Trim().ToString();
            return RedirectToAction("index");
        }
        public ActionResult Logout()
        {
            Session["AdminUserName"] = null;
            return RedirectToAction("Index", "HomePage");

        }
        public ActionResult AllCustomer()
        {
            if (Session["AdminUserName"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            var data = con.Customers.ToList();

            return View(data);
        }
        public ActionResult Orders ()
        {
            if (Session["AdminUserName"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            var data =con.Orders.Where(o => o.IsConfirmed == true).ToList();
            
            return View(data);
        }
        public ActionResult OrderDetails(int id)// order id
        {
            if (Session["AdminUserName"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            var order = con.Orders.Where(o => o.ID == id).SingleOrDefault();
            ViewBag.CustomerName = con.Customers.Where(c => c.ID == order.Customer_id).SingleOrDefault().Name;
            var data = con.SubOrders.Where(s => s.Order_id== order.ID).Include("Product").ToList();
            List<AdminOrderDetailsViewModel> viewData = new List<AdminOrderDetailsViewModel>();
            foreach (var item in data)
            {
                viewData.Add(new AdminOrderDetailsViewModel
                {
                    Name = item.Product.Name,
                    Amount = item.Amount ,
                    ProviderName = item.Product.ProviderName ,
                    Salary = item.Product.Salary,
                }
                );

            }

            return View(viewData);
        }
    }
}