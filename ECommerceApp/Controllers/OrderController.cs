using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceApp.DAL;
using ECommerceApp.Models;
using System.Net;
namespace ECommerceApp.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(int id)
        {
            using (OrderDAL service = new OrderDAL())
            {
              
                return View(service.GetAllOrder(id).ToList());
            }
             
        }
        public ActionResult Ordernew()
        {
            var oder1 = new Order
            {
                CustomerName = Session["username"].ToString(),
                OrderDate = DateTime.Now,
                ShipDate = DateTime.Now.AddDays(3),
                            
            };
            using (OrderDAL service = new OrderDAL())
            {
                service.AddOrder(oder1);
                foreach (var oder in service.GetAllData(Session["username"].ToString()).ToList())
                {
                var detailorder = new OrderDetail
                {
                    OrderID = oder1.OrderID,
                    BookID = oder.BookID,
                    Quantity = oder.Quantity,
                    Price = oder.Book.Price

                };
                    service.AddDetailOrder(detailorder);
                    service.RemoveCart(oder);
                }
            }

                return RedirectToAction("Index", new { id = oder1.OrderID });

            }

    }
}