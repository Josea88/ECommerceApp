using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ECommerceApp.DAL;
using ECommerceApp.Models;

namespace ECommerceApp.Controllers
{
    public class AuthorsController : Controller
    {
        // GET: Authors
        public ActionResult Index()
        {
            using(AuthorsDAL service = new AuthorsDAL())
            {
                var model = service.GetData().ToList();
                if (TempData["Pesan"] != null)
                {
                    ViewBag.Pesan = TempData["Pesan"].ToString();
                }
                return View(model);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            using (AuthorsDAL service = new AuthorsDAL())
            {
                try
                {
                    service.Add(author);
                    TempData["Pesan"] = Helpers.KotakPesan.GetPesan("Sukses !",
                        "success", "Data Author " + author.FirstName + " berhasil ditambah");
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
            using (AuthorsDAL services = new AuthorsDAL())
            {
                var result = services.GetDataById(id);
                return View(result);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            using (AuthorsDAL services = new AuthorsDAL())
            {
                try
                {
                    services.Edit(author);
                    TempData["Pesan"] = Helpers.KotakPesan.GetPesan("Sukses !",
                        "success", "Data Author " + author.FirstName + " berhasil diedit");
                }
                catch (Exception ex)
                {
                    TempData["Pesan"] = Helpers.KotakPesan.GetPesan("Error !",
                                         "danger", ex.Message);
                }
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            using (AuthorsDAL service = new AuthorsDAL())
            {
                try
                {
                    service.Delete(id);
                    TempData["Pesan"] = Helpers.KotakPesan.GetPesan("Sukses !",
                        "success", "Data Author berhasil didelete !");
                }
                catch (Exception ex)
                {
                    TempData["Pesan"] = Helpers.KotakPesan.GetPesan("Error !",
                                         "danger", ex.Message);
                }
            }
            return RedirectToAction("Index");
        }
    }
}