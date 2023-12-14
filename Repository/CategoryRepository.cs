using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    { 
        public void Add(Category category)
        {
            CategoryDAO.SingletonInstance.Add(category);
        }

        public void Delete(Category category)
        {
            CategoryDAO.SingletonInstance.Delete(category);
        }

        public IList<Category> GetAll()
        {
            return CategoryDAO.SingletonInstance.GetAll();
        }

        public Category? GetById(int id)
        {
            return CategoryDAO.SingletonInstance.GetById(id);
        }

        public void Update(Category category)
        {
            CategoryDAO.SingletonInstance.Update(category);
        }
    }
}
