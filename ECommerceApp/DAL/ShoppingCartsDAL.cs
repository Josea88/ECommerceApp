using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Models;

namespace ECommerceApp.DAL
{
    public class ShoppingCartsDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();

        public IQueryable<ShoppingCart> GetAllData(string username)
        {
            var results = from s in db.ShoppingCarts.Include("Book")
                          where s.CartID == username
                          orderby s.RecordID ascending
                          select s;
            return results;
        }

        //cek apakah barang dengan user yang sama ada di shopping cart
        public ShoppingCart GetItemByUser(string username,int bookId)
        {
            var result = (from sc in db.ShoppingCarts
                          where sc.CartID == username && sc.BookID == bookId
                          select sc).FirstOrDefault();

            return result;
        }

        public void UpdateCartID(string tempUsername, string username)
        {
            var results = from s in db.ShoppingCarts
                          where s.CartID == tempUsername
                          select s;

            foreach(var sc in results)
            {
                sc.CartID = username;
            }
            db.SaveChanges();
        }
        public ShoppingCart GetItemByID(int ID )
        {
            var result = (from shop in db.ShoppingCarts
                          where shop.RecordID == ID
                          select shop).SingleOrDefault();
            return result;
        }

        public void Edit(ShoppingCart obj)
        {
            var model = GetItemByID(obj.RecordID);
            if (model != null)
            {
                model.Quantity = obj.Quantity;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex )
                {

                    throw new Exception(ex.Message);
                }
            }

        }
        public void Delete(int ID)
        {
            var model = GetItemByID(ID);
            if (model != null)
            {
                db.ShoppingCarts.Remove(model);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
        public void AddToCart(ShoppingCart shoppingCart)
        {
            //cek apakah cart dengan pengguna dan buku sama sudah ada
            var result = GetItemByUser(shoppingCart.CartID, shoppingCart.BookID);
            if(result!=null)
            {
                //update
                result.Quantity += 1;
            }
            else
            {
                //tambah baru
                db.ShoppingCarts.Add(shoppingCart);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
