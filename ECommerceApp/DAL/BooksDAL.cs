
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECommerceApp.Models;
using ECommerceApp.ViewModels;

namespace ECommerceApp.DAL
{
    public class BooksDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();

        public IQueryable<Book> GetData()
        {
            var results = from b in db.Books
                          orderby b.Title
                          select b;

            return results;
        }

        public IQueryable<BookVM> GetBooksWithAuthors()
        {
            var results = from b in db.Books.Include("Author")
                          orderby b.Title
                          select new BookVM
                          {
                              BookID = b.BookID,
                              AuthorID = b.AuthorID,
                              Title = b.Title,
                              PublicationDate = b.PublicationDate,
                              ISBN = b.ISBN,
                              CoverImage = b.CoverImage,
                              Price = b.Price,
                              Description = b.Description,
                              Publisher = b.Publisher,
                              FirstName = b.Author.FirstName,
                              LastName = b.Author.LastName
                          };
            return results;
        }

        public BookVM GetDetailWithAuthors(int id)
        {
            var result = (from b in db.Books.Include("Authors")
                          where b.BookID == id
                          orderby b.Title
                          select new BookVM
                          {
                              BookID = b.BookID,
                              AuthorID = b.AuthorID,
                              Title = b.Title,
                              PublicationDate = b.PublicationDate,
                              ISBN = b.ISBN,
                              CoverImage = b.CoverImage,
                              Price = b.Price,
                              Description = b.Description,
                              Publisher = b.Publisher,
                              FirstName = b.Author.FirstName,
                              LastName = b.Author.LastName
                          }).SingleOrDefault();

            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("Data tidak ditemukan !");
            }
        }

        public IQueryable<BookVM> SearchByKriteria(string selectKriteria, 
            string txtSearch)
        {
            IQueryable<BookVM> results;
            if(selectKriteria=="Title")
            {
                results = from b in db.Books.Include("Authors")
                          where b.Title.ToLower().Contains(txtSearch.ToLower())
                          orderby b.Title
                          select new BookVM
                          {
                              BookID = b.BookID,
                              AuthorID = b.AuthorID,
                              Title = b.Title,
                              PublicationDate = b.PublicationDate,
                              ISBN = b.ISBN,
                              CoverImage = b.CoverImage,
                              Price = b.Price,
                              Description = b.Description,
                              Publisher = b.Publisher,
                              FirstName = b.Author.FirstName,
                              LastName = b.Author.LastName
                          };

            }
            else
            {
                results = from b in db.Books.Include("Authors")
                          where b.Author.FirstName.ToLower().Contains(txtSearch.ToLower()) || 
                          b.Author.LastName.ToLower().Contains(txtSearch.ToLower())
                          orderby b.Title
                          select new BookVM
                          {
                              BookID = b.BookID,
                              AuthorID = b.AuthorID,
                              Title = b.Title,
                              PublicationDate = b.PublicationDate,
                              ISBN = b.ISBN,
                              CoverImage = b.CoverImage,
                              Price = b.Price,
                              Description = b.Description,
                              Publisher = b.Publisher,
                              FirstName = b.Author.FirstName,
                              LastName = b.Author.LastName
                          };
            }
            return results;
        }

        public Book GetDataById(int id)
        {
            var result = (from b in db.Books
                          where b.BookID == id
                          select b).SingleOrDefault();

            return result;
        }

        public void Add(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }

        public void Edit(Book book)
        {
            var result = GetDataById(book.BookID);
            if (result != null)
            {
                result.AuthorID = book.AuthorID;
                result.CategoryID = book.CategoryID;
                result.Title = book.Title;
                result.PublicationDate = book.PublicationDate;
                result.ISBN = book.ISBN;
                result.CoverImage = book.CoverImage;
                result.Price = book.Price;
                result.Description = book.Description;
                result.Publisher = book.Publisher;

                db.SaveChanges();
            }
            else
            {
                throw new Exception("Data tidak ditemukan !");
            }
        }

        public void Delete(int id)
        {
            var result = GetDataById(id);
            if (result != null)
            {
                db.Books.Remove(result);
                db.SaveChanges();
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
