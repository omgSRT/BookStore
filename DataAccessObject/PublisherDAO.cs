using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class PublisherDAO
    {
        private static PublisherDAO _instance = null;
        private static readonly object _instanceLock = new object();
        public PublisherDAO() { }

        public static PublisherDAO SingletonInstance
        {
            get
            {
                lock( _instanceLock )
                {
                    if( _instance == null )
                    {
                        _instance = new PublisherDAO();
                    }
                    return _instance;
                }
            }
        }
        public void Add(Publisher publisher)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Publishers.Find(publisher.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Add(publisher);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Delete(Publisher publisher)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Publishers.Find(publisher.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Remove(publisher);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public IList<Publisher> GetAll()
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Publisher>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Publisher>();
            }
        }

        public Publisher? GetById(int id)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Publisher>().Find(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public void Update(Publisher publisher)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Publishers.Find(publisher.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Update(publisher);
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
