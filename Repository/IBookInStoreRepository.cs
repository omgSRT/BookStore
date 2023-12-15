using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBookInStoreRepository
    {
        void Add(BookInStore bookInStore);
        void Update(BookInStore bookInStore);
        void Delete(BookInStore bookInStore);
        IList<BookInStore> GetAll();
        BookInStore? GetById(int id);
        IList<BookInStore> GetAllWithIncludeBookAndStore();
        IList<BookInStore> GetByStoreId(int id);
    }
}
