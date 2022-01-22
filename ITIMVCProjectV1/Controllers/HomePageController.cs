using ITIMVCProjectV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIMVCProjectV1.ViewModel;
using System.IO;
using System.Net.Mail;

namespace ITIMVCProjectV1.Controllers
{
    public class HomePageController : Controller
    {
        DB Conn = new DB();
        private bool ChechSession()
        {
            return Session["CustomerUserName"] == null;
        }
        // GET: HomePage
        public ActionResult Index()
        {
            var products = Conn.Products.Include("Category").Where(p => p.Amount >0).ToList();
            List<ProductHomePageViewModel> data = new List<ProductHomePageViewModel>();
            foreach (var item in products)
            {
                data.Add(new ProductHomePageViewModel { 
                    Id=item.ID,
                    Name=item.Name,
                    Salary= item.Salary,
                    Image =Path.GetFileName(item.Image),
                    ProviderName = item.ProviderName,
                    CategoryType = item.Category.Type

                });
            }
            ViewBag.category = Conn.Categories.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(CustomerRegisterViewModel data)
        {
            if(! ModelState.IsValid)
            {
                return View();

            }

            Conn.Customers.Add(new Customer { Name=data.Name.Trim() , UserName = data.UserName.Trim(),Password=data.Password.Trim(),Phone =data.PhoneNumber.Trim() });
            Conn.SaveChanges();
            Session["CustomerUserName"] = data.UserName.ToString();

            return RedirectToAction("index", "HomePage");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CustomerLoginViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            var Customer = Conn.Customers.Where(c => c.UserName == data.UserName.Trim() && c.Password == data.Password.Trim()).SingleOrDefault();
            if(Customer==null)
            {
                return View();
            }
            
            Session["CustomerUserName"] = Customer.UserName.ToString();

            return RedirectToAction("index", "HomePage");
        }

        public ActionResult Logout()
        {
            Session["CustomerUserName"] = null;
            return RedirectToAction("index", "HomePage");
        }
        public ActionResult GetCategory(int id)
        {
            var products = Conn.Products.Include("Category").Where(p => p.Category_id == id).ToList();
            List<ProductHomePageViewModel> data = new List<ProductHomePageViewModel>();
            foreach (var item in products )
            {
                data.Add(new ProductHomePageViewModel
                {
                    Id = item.ID,
                    Name = item.Name,
                    Salary = item.Salary,
                    Image = Path.GetFileName(item.Image),
                    ProviderName = item.ProviderName,
                    CategoryType = item.Category.Type

                });
            }
            ViewBag.CategoryName = products[0].Category.Type;
            ViewBag.category = Conn.Categories.ToList();

            return View(data);

        }

        public ActionResult Details(int id)
        {
            var data = Conn.Products.Where(p => p.ID == id).FirstOrDefault();
            var feed = Conn.Feadbacks.Include("Customer").Where(f => f.Product.ID == id).ToList();
            ViewBag.ProductId = data.ID;
            ViewBag.Name = data.Name;
            ViewBag.Image = Path.GetFileName( data.Image);
            ViewBag.Salary = data.Salary;
            ViewBag.ProviderName = data.ProviderName;
            List<CustomerComment> d = new List<CustomerComment>();
            foreach (var item in feed)
            {
                d.Add(new CustomerComment
                {
                    Comment = item.Comment,
                    CustomerName = item.Customer.Name,

                    Rate = item.Rate,


                });
            }
            return View(d);
        }
        [HttpGet]
        public ActionResult AddComment(int id) // id == product id
        {
            string UserName = Session["CustomerUserName"].ToString();
            ViewBag.CustomerId = Conn.Customers.Where(c => c.UserName.Trim() == UserName.Trim() ).SingleOrDefault().ID;
            ViewBag.ProductId = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddComment (CustomerAddComment data )
        {

           if(! ModelState.IsValid)
            {
                return View(data.ProductId);
            }
            data.Rate = (data.Rate <= 0) ? 1 : data.Rate;
            Conn.Feadbacks.Add(new Feedback
            {
                Product_ID =data.ProductId,
                Customer_ID = data.CustomerId,
                Comment = data.Comment,
                Rate = (data.Rate >= 10) ? 10 :data.Rate

            });
            Conn.SaveChanges();
            return RedirectToAction("Details", "HomePage", new { id = data.ProductId }); 
        }
        [HttpGet]
        public ActionResult Bay(int id) // product Id
        {
          
            ViewBag.ProductId = id;

            return View();
           
        }
        [HttpPost]
        public ActionResult Bay( CustomerAddToCardViewModel data )
        {
            if (!ModelState.IsValid)
            {
                return View(data.ProductId);
            }

            string UserName = Session["CustomerUserName"].ToString();
            int CustomerId = Conn.Customers.Where(c => c.UserName.Trim() == UserName.Trim()).SingleOrDefault().ID;
            var x = DateTime.Now.Date;
            if(Conn.Orders.Where(o => o.Customer_id == CustomerId && o.IsConfirmed == false).Count() == 0)
            {

                Conn.Orders.Add(
                    new Order
                    {
                        Customer_id = CustomerId
                        
                    }
                    );
                Conn.SaveChanges();
            }

            int OrderId = Conn.Orders.Where(o => o.Customer_id == CustomerId && o.IsConfirmed == false).SingleOrDefault().ID;
            Conn.SubOrders.Add(new SubOrder
            {
                Order_id = OrderId ,
                Amount = data.Amount ,
                Product_id = data.ProductId
            });
            Conn.SaveChanges();
            return RedirectToAction("index", "HomePage");
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(vm.Email);//Email which you are getting 
                                                         //from contact us page 
                    msz.To.Add("yehiazakaria259@gmail.com");//Where mail will be sent 
                    msz.Subject = vm.Subject;
                    msz.Body = vm.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("yehiazakaria259@gmail.com", "251998yehia");

                    smtp.EnableSsl = true;

                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting us ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }
            }

            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        //ContactUs // About
    }
}