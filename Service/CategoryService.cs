using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _repository;
        public CategoryService()
        {
            _repository = new CategoryRepository();
        }

        public void Add(Category category)
        {
            _repository.Add(category);
        }

        public void Delete(Category category)
        {
            _repository.Delete(category);
        }

        public IList<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public Category? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Category category)
        {
            _repository.Update(category);
        }
    }
}
