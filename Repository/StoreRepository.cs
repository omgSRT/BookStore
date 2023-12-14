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
        public void Add(Store store)
        {
            StoreDAO.SingletonInstance.Add(store);
        }

        public void Delete(Store store)
        {
            StoreDAO.SingletonInstance.Delete(store);
        }

        public IList<Store> GetAll()
        {
            return StoreDAO.SingletonInstance.GetAll();
        }

        public Store? GetById(int id)
        {
            return StoreDAO.SingletonInstance.GetById(id);
        }

        public void Update(Store store)
        {
            StoreDAO.SingletonInstance.Update(store);
        }
    }
}
