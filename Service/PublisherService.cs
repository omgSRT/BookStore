using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PublisherService : IPublisherService
    {
        private IPublisherRepository _repository;
        public PublisherService()
        {
            _repository = new PublisherRepository();
        }

        public void Add(Publisher publisher)
        {
            _repository.Add(publisher);
        }

        public void Delete(Publisher publisher)
        {
            _repository.Delete(publisher);
        }

        public IList<Publisher> GetAll()
        {
            return _repository.GetAll();
        }

        public Publisher? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Publisher publisher)
        {
            _repository.Update(publisher);
        }
    }
}
