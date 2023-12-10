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
        private BookInStoreDAO _dao;
        public BookInStoreRepository(BookInStoreDAO dao)
        {
            _dao = dao;
        }

        public void Add(BookInStore bookInStore)
        {
            _dao.Add(bookInStore);
        }

        public void Delete(BookInStore bookInStore)
        {
            _dao.Delete(bookInStore);
        }

        public IEnumerable<BookInStore> GetAll()
        {
            return _dao.GetAll();
        }

        public BookInStore? GetById(int id)
        {
            return _dao.GetById(id);
        }

        public void Update(BookInStore bookInStore)
        {
            _dao.Update(bookInStore);
        }
    }
}
