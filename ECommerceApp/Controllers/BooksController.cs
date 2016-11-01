using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ECommerceApp.DAL;
using ECommerceApp.Models;
using ECommerceApp.ViewModels;

using System.IO;

namespace ECommerceApp.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            using (BooksDAL svBooks = new BooksDAL())
            {
                var results = svBooks.GetData();
                return View(results);
            }    
        }

        public ActionResult ViewAllBooks()
        {
            using (BooksDAL svBooks = new BooksDAL())
            {
                var results = svBooks.GetBooksWithAuthors().ToList();
                return View(results);
            }
        }

        public ActionResult Details(int id)
        {
            using (BooksDAL svBooks = new BooksDAL())
            {
                var result = svBooks.GetDetailWithAuthors(id);
                return View(result);
            }
        }

        public ActionResult Search(string selectKriteria,
            string txtSearch)
        {
            using(BooksDAL svBooks = new BooksDAL())
            {
                var results = svBooks.SearchByKriteria(selectKriteria, txtSearch).ToList();

                return View("ViewAllBooks", results);
            }
        }

        public ActionResult Create()
        {
            //data author
            var lstAuthor = new List<SelectListItem>();
            using (AuthorsDAL svAuthors = new AuthorsDAL())
            {
                foreach (var author in svAuthors.GetData())
                {
                    lstAuthor.Add(new SelectListItem
                    {
                        Value = author.AuthorID.ToString(),
                        Text = author.FirstName + " " + author.LastName
                    });
                }
                ViewBag.Authors = lstAuthor;
            }

            //data category
            var lstCategories = new List<SelectListItem>();
            using (CategoriesDAL svCategories = new CategoriesDAL())
            {
                foreach (var category in svCategories.GetData())
                {
                    lstCategories.Add(new SelectListItem
                    {
                        Value = category.CategoryID.ToString(),
                        Text = category.CategoryName
                    });
                }
                ViewBag.Categories = lstCategories;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book,HttpPostedFileBase uploadimage)
        {
            string filePath = "";
            if(uploadimage.ContentLength > 0)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + uploadimage.FileName;
                filePath = Path.Combine(HttpContext.Server.MapPath("~/Content/Images"),fileName);
                uploadimage.SaveAs(filePath);

                book.CoverImage = fileName;
            }

            using (BooksDAL svBooks = new BooksDAL())
            {
                try
                {
                    svBooks.Add(book);
                    TempData["Pesan"] = Helpers.KotakPesan.GetPesan("Sukses !",
                       "success", "Data Book " + book.Title + " berhasil ditambah");
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