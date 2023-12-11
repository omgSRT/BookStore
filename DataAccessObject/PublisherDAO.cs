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
        private BookStoreDBContext _context;
        public PublisherDAO()
        {
            _context = new BookStoreDBContext();
        }
        public void Add(Publisher publisher)
        {
            try
            {
                var checkExist = _context.Publishers.Find(publisher.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Add(publisher);
                _context.SaveChanges();
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
                var checkExist = _context.Publishers.Find(publisher.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Remove(publisher);
                _context.SaveChanges();
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
                return _context.Set<Publisher>().ToList();
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
                return _context.Set<Publisher>().Find(id);
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
                var checkExist = _context.Publishers.Find(publisher.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Update(publisher);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
