using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IStoreService
    {
        void Add(Store store);
        void Update(Store store);
        void Delete(Store store);
        IList<Store> GetAll();
        Store? GetById(int id);
    }
}
