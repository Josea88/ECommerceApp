using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Models;

namespace ECommerceApp.DAL
{
    public class AuthorsDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();

        public IQueryable<Author> GetData()
        {
            var results = from a in db.Authors
                          orderby a.FirstName
                          select a;
            return results;
        }

        public Author GetDataById(int id)
        {
            var result = (from a in db.Authors
                          where a.AuthorID == id
                          select a).SingleOrDefault();

            return result;
        }

        public void Add(Author obj)
        {
            try
            {
                db.Authors.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Edit(Author obj)
        {
            var model = GetDataById(obj.AuthorID);

            if(model!=null)
            {
                model.FirstName = obj.FirstName;
                model.LastName = obj.LastName;
                model.Email = obj.Email;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Data tidak ditemukan !");
            }
        }

        public void Delete(int id)
        {
            var model = GetDataById(id);
            if(model!=null)
            {
                try
                {
                    db.Authors.Remove(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message,ex.InnerException);
                }
            }
            else
            {
                throw new Exception("Data tidak ditemukan !");
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
