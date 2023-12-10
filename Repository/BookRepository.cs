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
        private BookDAO _dao;
        public BookRepository(BookDAO dao)
        {
            _dao = dao;
        }

        public void Add(Book book)
        {
            _dao.Add(book);
        }

        public void Delete(Book book)
        {
            _dao.Delete(book);
        }

        public IEnumerable<Book> GetAll()
        {
            return _dao.GetAll();
        }

        public Book? GetById(int id)
        {
            return _dao.GetById(id);
        }

        public void Update(Book book)
        {
            _dao.Update(book);
        }
    }
}
