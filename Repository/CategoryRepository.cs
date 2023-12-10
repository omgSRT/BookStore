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
        private CategoryDAO _dao;
        public CategoryRepository(CategoryDAO dao)
        {
            _dao = dao;
        }

        public void Add(Category category)
        {
            _dao.Add(category);
        }

        public void Delete(Category category)
        {
            _dao.Delete(category);
        }

        public IEnumerable<Category> GetAll()
        {
            return _dao.GetAll();
        }

        public Category? GetById(int id)
        {
            return _dao.GetById(id);
        }

        public void Update(Category category)
        {
            _dao.Update(category);
        }
    }
}
