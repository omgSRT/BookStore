using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class StoreDAO
    {
        private static StoreDAO _instance = null;
        private static readonly object _instanceLock = new object();
        public StoreDAO() { }

        public static StoreDAO SingletonInstance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new StoreDAO();
                    }
                    return _instance;
                }
            }
        }
        public void Add(Store store)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Stores.Find(store.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Add(store);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Delete(Store store)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Stores.Find(store.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Remove(store);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public IList<Store> GetAll()
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Store>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Store>();
            }
        }

        public Store? GetById(int id)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Store>().Find(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public void Update(Store store)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Stores.Find(store.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Update(store);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
