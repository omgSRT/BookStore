using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class BookInStoreDAO
    {
        private static BookInStoreDAO _instance = null;
        private static readonly object _instanceLock = new object();
        public BookInStoreDAO() { }

        public static BookInStoreDAO SingletonInstance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new BookInStoreDAO();
                    }
                    return _instance;
                }
            }
        }
        public void Add(BookInStore bookInStore)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.BookInStores.Find(bookInStore.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Add(bookInStore);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Delete(BookInStore bookInStore)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.BookInStores.Find(bookInStore.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Remove(bookInStore);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public IList<BookInStore> GetAll()
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<BookInStore>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<BookInStore>();
            }
        }

        public BookInStore? GetById(int id)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<BookInStore>().Find(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public void Update(BookInStore bookInStore)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.BookInStores.Find(bookInStore.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Update(bookInStore);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public IList<BookInStore> GetAllWithIncludeBookAndStore()
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<BookInStore>()
                    .Include(bis => bis.Book)
                    .Include(bis => bis.Book!.Category)
                    .Include(bis => bis.Book!.Publisher)
                    .Include(bis => bis.Store)
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<BookInStore>();
            }
        }
    }
}
