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
        private static BookDAO _instance = null;
        private static readonly object _instanceLock = new object();
        public BookDAO() { }

        public static BookDAO SingletonInstance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new BookDAO();
                    }
                    return _instance;
                }
            }
        }
        public void Add(Book book)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Books.Find(book.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Add(book);
                    _context.SaveChanges();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Books.Find(book.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Remove(book);
                    _context.SaveChanges();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Book>().ToList();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Book>().Find(id);
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Books.Find(book.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Update(book);
                    _context.SaveChanges();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Book>()
                    .Include("Category")
                    .Include("Publisher")
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Book>();
            }
        }
    }
}
