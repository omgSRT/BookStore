using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class BookDAO
    {
        private BookStoreDBContext _context;
        public BookDAO()
        {
            _context = new BookStoreDBContext();
        }
        public void Add(Book book)
        {
            try
            {
                var checkExist = _context.Books.Find(book.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Delete(Book book)
        {
            try
            {
                var checkExist = _context.Books.Find(book.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Remove(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public IList<Book> GetAll()
        {
            try
            {
                return _context.Set<Book>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Book>();
            }
        }

        public Book? GetById(int id)
        {
            try
            {
                return _context.Set<Book>().Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public void Update(Book book)
        {
            try
            {
                var checkExist = _context.Books.Find(book.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Update(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public IList<Book> GetAllWithIncludeCategoryAndPublisher()
        {
            try
            {
                return _context.Set<Book>()
                    .Include("Category")
                    .Include("Publisher")
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Book>();
            }
        }
    }
}
