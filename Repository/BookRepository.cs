using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookRepository : IBookRepository
    { 
        public void Add(Book book)
        {
            BookDAO.SingletonInstance.Add(book);
        }

        public void Delete(Book book)
        {
            BookDAO.SingletonInstance.Delete(book);
        }

        public IList<Book> GetAll()
        {
            return BookDAO.SingletonInstance.GetAll();
        }

        public Book? GetById(int id)
        {
            return BookDAO.SingletonInstance.GetById(id);
        }

        public void Update(Book book)
        {
            BookDAO.SingletonInstance.Update(book);
        }
        public IList<Book> GetAllWithIncludeCategoryAndPublisher()
        {
            return BookDAO.SingletonInstance.GetAllWithIncludeCategoryAndPublisher();
        }
    }
}
