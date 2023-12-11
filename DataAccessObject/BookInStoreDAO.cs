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
        private BookStoreDBContext _context;
        public BookInStoreDAO()
        {
            _context = new BookStoreDBContext();
        }
        public void Add(BookInStore bookInStore)
        {
            try
            {
                var checkExist = _context.BookInStores.Find(bookInStore.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Add(bookInStore);
                _context.SaveChanges();
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
                var checkExist = _context.BookInStores.Find(bookInStore.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Remove(bookInStore);
                _context.SaveChanges();
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
                return _context.Set<BookInStore>().ToList();
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
                return _context.Set<BookInStore>().Find(id);
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
                var checkExist = _context.BookInStores.Find(bookInStore.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Update(bookInStore);
                _context.SaveChanges();
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
                return _context.Set<BookInStore>()
                    .Include("Book")
                    .Include("Store")
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<BookInStore>();
            }
        }
    }
}
