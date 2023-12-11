using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StoreRepository : IStoreRepository
    {
        private StoreDAO _dao;
        public StoreRepository()
        {
            _dao = new StoreDAO();
        }

        public void Add(Store store)
        {
            _dao.Add(store);
        }

        public void Delete(Store store)
        {
            _dao.Delete(store);
        }

        public IList<Store> GetAll()
        {
            return _dao.GetAll();
        }

        public Store? GetById(int id)
        {
            return _dao.GetById(id);
        }

        public void Update(Store store)
        {
            _dao.Update(store);
        }
    }
}
