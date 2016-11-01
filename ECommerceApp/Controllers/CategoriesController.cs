using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ECommerceApp.Models;
using ECommerceApp.DAL;
using System.Net;

namespace ECommerceApp.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            using (CategoriesDAL service = new CategoriesDAL())
            {
                var categories = service.GetData().ToList();
                if(TempData["Pesan"]!=null)
                {
                    ViewBag.Pesan = TempData["Pesan"].ToString();
                }
                return View(categories);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            using (CategoriesDAL service = new CategoriesDAL())
            {
                try
                {
                    service.Add(category);
                    TempData["Pesan"] = Helpers.KotakPesan.GetPesan("Sukses !",
                        "success", "Data kategori " + category.CategoryName + " berhasil ditambah");
                }
                catch (Exception ex)
                {
                    TempData["Pesan"] = Helpers.KotakPesan.GetPesan("Error !",
                        "danger", ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            using (CategoriesDAL service = new CategoriesDAL())
            {
                var category = service.GetDataByID(id);
                return View(category);
            }       
        }

        public ActionResult Search(string txtSearch)
        {
            using (CategoriesDAL svBooks = new CategoriesDAL())
            {
                var results = svBooks.Search(txtSearch).ToList();

                return View("ViewAllCategories", results);
            }
        }

        [HttpPost,ActionName("Edit")]
        public ActionResult EditPost(int? id,Category category)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (CategoriesDAL service = new CategoriesDAL())
            {
                try
                {
                    service.Edit(id.Value, category);
                    TempData["Pesan"] = Helpers.KotakPesan.GetPesan("Sukses !",
                        "success", "Data kategori " + category.CategoryName + " berhasil diupdate");
                }
                catch (Exception ex)
                {
                    TempData["Pesan"] = Helpers.KotakPesan.GetPesan("Error !",
                        "danger", ex.Message);
                }
            }
            
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? id)
        {
            if(id != null)
            {
                using (CategoriesDAL service = new CategoriesDAL())
                {
                    try
                    {
                        service.Delete(id.Value);
                        TempData["Pesan"] = Helpers.KotakPesan.GetPesan("Sukses !",
                            "success", "Data kategori berhasil di hapus !");
                    }
                    catch (Exception ex)
                    {
                        TempData["Pesan"] = Helpers.KotakPesan.GetPesan("Error !",
                        "danger", ex.Message);
                    }
                }
            }
            return RedirectToAction("Index");
        }

    }
}