using ITIMVCProjectV1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIMVCProjectV1.ViewModel;
using System;

using System.IO;

namespace ITIMVCProjectV1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DB conn = new DB();

        private bool CheckSeesion()
        {
            return Session["AdminUserName"] == null;
        }
        public ActionResult Index()
        {
            if (CheckSeesion())
            {
                return RedirectToAction("Login", "Admin");
            }

            var data = conn.Products.Include("Category").ToList();
            var viewData = new List<AdminProductViewModel>();
            foreach (var item in data)
            {
                viewData.Add(
                    new AdminProductViewModel
                    {
                        ID = item.ID,
                        Name =item.Name,
                        Cost =item.Cost,
                        Salary= item.Salary,
                        Image = Path.GetFileName( item.Image),
                        Amount = item.Amount,
                        TotleAmount = item.TotleAmount,
                        ProviderName = item.ProviderName,
                        Category_id = item.Category_id,
                        Category =item.Category
                    }
                    );
            }

            return View(viewData);
        }
        [HttpGet]
        public ActionResult Add()
        {
            if (CheckSeesion())
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.Category = conn.Categories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(ProductProcessViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if(data.UploadedFile == null || data.UploadedFile.ContentLength==0)
            {
                return View();

            }

            var extention = Path.GetExtension(data.UploadedFile.FileName);
            var Name = DateTime.Now.ToString("dddd_dd_MMMM_yyyy_HH_mm_ss");
            var imageName = Name + extention;
            string path = Server.MapPath($"~/Image/{imageName}");
            data.UploadedFile.SaveAs(path);

            Product p = new Product()
            {
                Name = data.Name,
                Cost = data.Cost,
                Salary = data.Salary,
                Image = path,
                Amount =data.TotleAmount ,
                TotleAmount = data.TotleAmount,
                ProviderName = data.ProviderName ,
                Category_id =data.Category_id
              
            };
            conn.Products.Add(p);
            conn.SaveChanges();
            return RedirectToAction("index", "product");
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (CheckSeesion())
            {
                return RedirectToAction("Login", "Admin");
            }
            var date = conn.Products.Where(p => p.ID == id).FirstOrDefault();
            AdminEditProductViewModel dataView = new AdminEditProductViewModel()
            {
                Id = date.ID,
                Name = date.Name,
                Cost = date.Cost,
                Salary = date.Salary,
                Image = Path.GetFileName(date.Image),
                TotleAmount = date.Amount,
                ProviderName = date.ProviderName,
                Category_id = date.Category_id
            };
            ViewBag.Category = conn.Categories.ToList();
            return View(dataView);

        }
        [HttpPost]
        public ActionResult Edit(AdminEditProductViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data.Id);
            }
            if (data.UploadedFile == null || data.UploadedFile.ContentLength == 0)
            {
                return View(data.Id);

            }
            var Product = conn.Products.Where(p => p.ID == data.Id).SingleOrDefault();
            var extention = Path.GetExtension(data.UploadedFile.FileName);
            var Name = DateTime.Now.ToString("dddd_dd_MMMM_yyyy_HH_mm_ss");
            var imageName = Name + extention;
            string path = Server.MapPath($"~/Image/{imageName}");
            data.UploadedFile.SaveAs(path);

            int NewAmount = data.TotleAmount;
            int diff = NewAmount - Product.Amount;


            Product.Name = data.Name;
            Product.Cost = data.Cost;
            Product.Salary = data.Salary;
            Product.Image = path;
            Product.Amount += diff ;
            Product.TotleAmount += diff;
            Product.ProviderName = data.ProviderName;
            Product.Category_id = data.Category_id;

            
            conn.SaveChanges();

            return RedirectToAction("Index", "product");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (CheckSeesion())
            {
                return RedirectToAction("Login", "Admin");
            }
            var date = conn.Products.Where(p => p.ID == id).SingleOrDefault();
            AdminDeleteProduct ProductView = new AdminDeleteProduct()
            {
                Id = date.ID,
                Name = date.Name,
                Cost = date.Cost,
                Salary = date.Salary,
                ProviderName = date.ProviderName,
                Image =Path.GetFileName( date.Image)
            };
            return View(ProductView);
        }
        [HttpPost]
        public ActionResult Delete(AdminDeleteProduct data)
        {
            if (!ModelState.IsValid)
            {
                return View(data.Id);
            }

            var DataDb = conn.Products.Where(p => p.ID == data.Id).SingleOrDefault();
            conn.Products.Remove(DataDb);
            conn.SaveChanges();

            return RedirectToAction("index", "Product");
        }
    }
}