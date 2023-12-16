using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BookService : IBookService
    {
        private IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public void Add(Book book)
        {
            _repository.Add(book);
        }

        public void Delete(Book book)
        {
            _repository.Delete(book);
        }

        public IList<Book> GetAll()
        {
            return _repository.GetAll();
        }

        public Book? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Book book)
        {
            _repository.Update(book);
        }
        public IList<Book> GetAllWithIncludeCategoryAndPublisher()
        {
            return _repository.GetAllWithIncludeCategoryAndPublisher();
        }

        public List<Book> GetByName(string name)
        {
            return _repository.GetByName(name);
        }
    }
}
