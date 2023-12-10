using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class CategoryDAO
    {
        private BookStoreDBContext _context;
        public CategoryDAO(BookStoreDBContext context)
        {
            _context = context;
        }
        public void Add(Category category)
        {
            try
            {
                var checkExist = _context.Categories.Find(category.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Add(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Delete(Category category)
        {
            try
            {
                var checkExist = _context.Categories.Find(category.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public IList<Category> GetAll()
        {
            try
            {
                return _context.Set<Category>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Category>();
            }
        }

        public Category? GetById(int id)
        {
            try
            {
                return _context.Set<Category>().Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public void Update(Category category)
        {
            try
            {
                var checkExist = _context.Categories.Find(category.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Update(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
