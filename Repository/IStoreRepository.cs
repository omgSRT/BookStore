using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IStoreRepository
    {
        void Add(Store store);
        void Update(Store store);
        void Delete(Store store);
        IEnumerable<Store> GetAll();
        Store? GetById(int id);
    }
}
