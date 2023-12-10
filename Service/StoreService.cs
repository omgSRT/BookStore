using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class StoreService : IStoreService
    {
        private IStoreRepository _repository;
        public StoreService(IStoreRepository repository)
        {
            _repository = repository;
        }

        public void Add(Store store)
        {
            _repository.Add(store);
        }

        public void Delete(Store store)
        {
            _repository.Delete(store);
        }

        public IList<Store> GetAll()
        {
            return _repository.GetAll();
        }

        public Store? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Store store)
        {
            _repository.Update(store);
        }
    }
}
