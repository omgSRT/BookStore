using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        public void Add(Publisher publisher)
        {
            PublisherDAO.SingletonInstance.Add(publisher);
        }

        public void Delete(Publisher publisher)
        {
            PublisherDAO.SingletonInstance.Delete(publisher);
        }

        public IList<Publisher> GetAll()
        {
            return PublisherDAO.SingletonInstance.GetAll();
        }

        public Publisher? GetById(int id)
        {
            return PublisherDAO.SingletonInstance.GetById(id);
        }

        public void Update(Publisher publisher)
        {
            PublisherDAO.SingletonInstance.Update(publisher);
        }
    }
}
