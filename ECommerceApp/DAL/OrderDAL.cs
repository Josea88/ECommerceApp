using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Models;
using ECommerceApp.ViewModels;
using System.Net;
namespace ECommerceApp.DAL
{
    public class OrderDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();

        public IQueryable<ShoppingCart> GetAllData(string id)
        {
            var results = (from s in db.ShoppingCarts
                          where s.CartID == id
                          select s);
            return results;
        }

        public IQueryable <OrderVM> GetAllOrder (int id)
        {
            var result = from s in db.OrderDetails.Include("Orders")
                         where s.OrderID == id
                         select new OrderVM
                         {
                             OrderID = s.Order.OrderID,
                             CustomerName = s.Order.CustomerName,
                             ShipDate = s.Order.ShipDate,
                             OrderDate = s.Order.OrderDate,
                             BookID = s.Book.BookID,
                             Quantity =s.Quantity,
                             Title = s.Book.Title,
                             Price = s.Book.Price
                         };

            return result;
        }



        public void AddOrder(Order obj)
        {
            try
            {
                db.Orders.Add(obj);
                db.SaveChanges();
            }
            catch (InvalidCastException e)
            {
                // Perform some action here, and then throw a new exception.
                throw new Exception("Put your error message here.", e);
            }
        }


        public void AddDetailOrder(OrderDetail obj)
        {
            try
            {
                db.OrderDetails.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception (ex.Message);
            }
        }

        public void RemoveCart (ShoppingCart obj)
        {
            try
            {
                db.ShoppingCarts.Remove(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }



    }
}