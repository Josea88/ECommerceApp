using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ECommerceApp.DAL;
using ECommerceApp.Models;

namespace ECommerceApp.Controllers
{
    public class ShoppingCartsController : Controller
    {
        // GET: ShoppingCarts
        public ActionResult Index()
        {
            using (ShoppingCartsDAL scService = new ShoppingCartsDAL())
            {
                string username = 
                    Session["username"] != null ? Session["username"].ToString() : string.Empty;
                return View(scService.GetAllData(username).ToList());
            }
        }

        public ActionResult AddToCart(int id)
        {
            //cek apakah user sudah login
            if(Session["username"]==null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Session["username"] = User.Identity.Name;
                }
                else
                {
                    var tempUser = Guid.NewGuid().ToString();
                    Session["username"] = tempUser;
                }
            }
            
            using (ShoppingCartsDAL scService = new ShoppingCartsDAL())
            {
                var newShoppingCart = new ShoppingCart
                {
                    CartID = Session["username"].ToString(),
                    Quantity = 1,
                    BookID = id,
                    DateCreated = DateTime.Now
                };
                scService.AddToCart(newShoppingCart);
            }

            return RedirectToAction("Index");
            
        }
        public ActionResult Edit(int ID)
        {
            using (ShoppingCartsDAL service = new ShoppingCartsDAL())
            {
                var a = service.GetItemByID(ID);
                return View(a); 
            }
                
        }

        public ActionResult Delete(int id)
        {
            using (ShoppingCartsDAL service = new ShoppingCartsDAL())
            {
                try
                {
                    service.Delete(id);
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit (ShoppingCart a)
        {
            using (ShoppingCartsDAL service = new ShoppingCartsDAL())
            {
                try
                {
                    service.Edit(a);
                }
                catch (Exception ex)
                {

                    throw new Exception (ex.Message);
                }
                return RedirectToAction("Index");
            }
        }
        public ActionResult Checkout()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (ShoppingCartsDAL service = new ShoppingCartsDAL())
                {
                    string username = Session["username"] != null ? Session["username"].ToString() : string.Empty;
                    return View(service.GetAllData(username).ToList());
                }

                
            }
            else
            {
                return RedirectToAction ("Login","Account");
            }
              
        }
      
        }
    }
