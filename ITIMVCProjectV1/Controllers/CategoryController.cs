using ITIMVCProjectV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITIMVCProjectV1.ViewModel;
namespace ITIMVCProjectV1.Controllers
{
    public class CategoryController : Controller
    {
        DB conn = new DB();
        // GET: Category
        private bool CheckSeesion()
        {
            return Session["AdminUserName"] == null ;
        }
        public ActionResult Index()
        {
            if (CheckSeesion())
            {
                return RedirectToAction("Login", "Admin");
            }
            var date = conn.Categories.ToList();
            var dataVm = new List<CategoryNameViewModel>();
            foreach (var item in date)
            {
                dataVm.Add(new CategoryNameViewModel
                {
                    Category_id = item.ID,
                    CategoryName = item.Type
                });
            }
            return View(dataVm);
        }

        [HttpGet]
        public ActionResult Add()
        {
            if (CheckSeesion())
            {
                return RedirectToAction("Login", "Admin");
            }

            return View();
        }
        [HttpPost]
        public ActionResult Add(CategoryNameOnly categoery )
        {
            if(! ModelState.IsValid)
            {
                return View();
            }
            conn.Categories.Add(
                new Category { Type = categoery.Categoryname.Trim()}
                );
            conn.SaveChanges();

            return RedirectToAction("index", "Category");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if(CheckSeesion())
            {
                return RedirectToAction("Login", "Admin");

            }
            var dataDb = conn.Categories.Where(c => c.ID == id).SingleOrDefault();
            if(dataDb == null)
            {
                return RedirectToAction("index", "category");

            }
            var data = new CategoryNameViewModel
            {
                CategoryName = dataDb.Type,
                Category_id = dataDb.ID
            };
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(CategoryNameViewModel categoery)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var data = conn.Categories.Where(c => c.ID == categoery.Category_id).SingleOrDefault();
            data.Type = categoery.CategoryName.Trim();
            conn.SaveChanges();
            return RedirectToAction("index", "Category");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (CheckSeesion())
            {
                return RedirectToAction("Login", "Admin");

            }
            var dataDb = conn.Categories.Where(c => c.ID == id).SingleOrDefault();
            if (dataDb == null)
            {
                return RedirectToAction("index", "category");

            }
            var data = new CategoryNameViewModel
            {
                CategoryName = dataDb.Type,
                Category_id = dataDb.ID
            };
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(CategoryNameViewModel categoery)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var data = conn.Categories.Where(c => c.ID == categoery.Category_id).SingleOrDefault();
            conn.Categories.Remove(data);
            conn.SaveChanges();
            return RedirectToAction("index", "Category");
        }
    }
}