using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IPublisherService
    {
        void Add(Publisher publisher);
        void Update(Publisher publisher);
        void Delete(Publisher publisher);
        IList<Publisher> GetAll();
        Publisher? GetById(int id);
    }
}
