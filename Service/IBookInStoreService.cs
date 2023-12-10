using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IBookInStoreService
    {
        void Add(BookInStore bookInStore);
        void Update(BookInStore bookInStore);
        void Delete(BookInStore bookInStore);
        IList<BookInStore> GetAll();
        BookInStore? GetById(int id);
        IList<BookInStore> GetAllWithIncludeBookAndStore();
    }
}
