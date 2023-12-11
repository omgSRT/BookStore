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
        private PublisherDAO _dao;
        public PublisherRepository()
        {
            _dao = new PublisherDAO();
        }

        public void Add(Publisher publisher)
        {
            _dao.Add(publisher);
        }

        public void Delete(Publisher publisher)
        {
            _dao.Delete(publisher);
        }

        public IList<Publisher> GetAll()
        {
            return _dao.GetAll();
        }

        public Publisher? GetById(int id)
        {
            return _dao.GetById(id);
        }

        public void Update(Publisher publisher)
        {
            _dao.Update(publisher);
        }
    }
}
