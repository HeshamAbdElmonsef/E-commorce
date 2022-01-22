using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIMVCProjectV1.ViewModel;
using ITIMVCProjectV1.Models;
using System.Data.Entity;

namespace ITIMVCProjectV1.Controllers
{
    public class CustomerController : Controller
    {
        DB con = new DB();
        private bool ChechSession()
        {
            return Session["CustomerUserName"] == null;
        }
        // GET: Customer
        public ActionResult Index()
        {
            if(ChechSession())
            {
                return RedirectToAction("Login", "HomePage");
            }
            var UserName = Session["CustomerUserName"].ToString();
            var data = con.Customers.Where(c => c.UserName == UserName).FirstOrDefault();
            if (data != null)
            {
                return View(data);
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = con.Customers.Where(c => c.ID == id).FirstOrDefault();
            EditCustomerViewModel edit = new EditCustomerViewModel()
            {
                ID = data.ID,
                Name = data.Name,
                UserName = data.UserName,
                Password = data.Password,
                Phone = data.Phone
            };
            if (data != null)
            {
                return View(edit);

            }
            return View();
        }
        [HttpPost]
        public ActionResult Edit(EditCustomerViewModel cust)
        {
            if (ModelState.IsValid)
            {
                var data = con.Customers.Where(c => c.ID == cust.ID).FirstOrDefault();
                if (data != null)
                {
                    data.ID = cust.ID;
                    data.Name = cust.Name;
                    data.UserName = cust.UserName;
                    data.Password = cust.Password;
                    data.Phone = cust.Phone;
                }
                con.SaveChanges();
                return RedirectToAction("Index");
            }


            return View();
        }

        public ActionResult orders ()
        {
            var UserName = Session["CustomerUserName"].ToString();
            var Customer = con.Customers.Where(c => c.UserName == UserName).FirstOrDefault();

            var data = con.Orders.Where(o => o.Customer_id == Customer.ID && o.IsConfirmed == false).ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult SubmitOrder(int id)   // order  id
        {
            var order = con.Orders.Where(o => o.ID == id).SingleOrDefault();

            var data = con.SubOrders.Where(s => s.Order_id == id).Include("Product").ToList();
            List<BillViewModel> bill = new List<BillViewModel>();
            foreach (var item in data)
            {
                bill.Add(new BillViewModel
                {
                    Name = item.Product.Name,
                    Amount = item.Amount,
                    Cost = item.Amount * item.Product.Salary,
                    ProviderName = item.Product.ProviderName
                });
            }
            double sum = bill.Sum(b => b.Cost);
            ViewBag.totalCost = sum;
            ViewBag.orderId = id;
            ViewBag.ReservationDate = DateTime.Now;
            ViewBag.deleveryDate = DateTime.Now.AddDays(3);
            ViewBag.Cost = sum;

            return View(bill);
        }
        public ActionResult Confirm(CustomerConfirmViewModel data )
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("SubmitOrder", "customer");

            }
            var order= con.Orders.Where(o => o.ID == data.Order_id).SingleOrDefault();
            order.IsConfirmed = true;
            order.Cost = data.Cost;
            order.DeliveryDate = data.DelveryDate;
            order.destination = data.Destination;
            order.destination = data.Destination;
            con.SaveChanges();
            var subordersAndProduct = con.SubOrders.Where(s => s.Order_id == data.Order_id).Include("Product").ToList();
            foreach (var item in subordersAndProduct)
            {
                item.Product.Amount -= item.Amount;
            }
            con.SaveChanges();


            return RedirectToAction("index", "Customer");
        }
    }
}