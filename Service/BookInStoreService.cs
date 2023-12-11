using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BookInStoreService : IBookInStoreService
    {
        private IBookInStoreRepository _repository;
        public BookInStoreService()
        {
            _repository = new BookInStoreRepository();
        }

        public void Add(BookInStore bookInStore)
        {
            _repository.Add(bookInStore);
        }

        public void Delete(BookInStore bookInStore)
        {
            _repository.Delete(bookInStore);
        }

        public IList<BookInStore> GetAll()
        {
            return _repository.GetAll();
        }

        public BookInStore? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(BookInStore bookInStore)
        {
            _repository.Update(bookInStore);
        }
        public IList<BookInStore> GetAllWithIncludeBookAndStore()
        {
            return _repository.GetAllWithIncludeBookAndStore();
        }
    }
}
