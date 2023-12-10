using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IPublisherRepository
    {
        void Add(Publisher publisher);
        void Update(Publisher publisher);
        void Delete(Publisher publisher);
        IEnumerable<Publisher> GetAll();
        Publisher? GetById(int id);
    }
}
