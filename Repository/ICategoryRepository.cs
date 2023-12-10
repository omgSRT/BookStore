using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        IEnumerable<Category> GetAll();
        Category? GetById(int id);
    }
}
