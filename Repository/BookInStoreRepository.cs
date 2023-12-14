using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookInStoreRepository : IBookInStoreRepository
    {
        public void Add(BookInStore bookInStore)
        {
            BookInStoreDAO.SingletonInstance.Add(bookInStore);
        }

        public void Delete(BookInStore bookInStore)
        {
            BookInStoreDAO.SingletonInstance.Delete(bookInStore);
        }

        public IList<BookInStore> GetAll()
        {
            return BookInStoreDAO.SingletonInstance.GetAll();
        }

        public BookInStore? GetById(int id)
        {
            return BookInStoreDAO.SingletonInstance.GetById(id);
        }

        public void Update(BookInStore bookInStore)
        {
            BookInStoreDAO.SingletonInstance.Update(bookInStore);
        }
        public IList<BookInStore> GetAllWithIncludeBookAndStore()
        {
            return BookInStoreDAO.SingletonInstance.GetAllWithIncludeBookAndStore();
        }
    }
}
