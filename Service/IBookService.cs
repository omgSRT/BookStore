using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IBookService
    {
        void Add(Book book);
        void Delete(Book book);
        void Update(Book book);
        IList<Book> GetAll();
        Book? GetById(int id);
        IList<Book> GetAllWithIncludeCategoryAndPublisher();
        List<Book> GetByName(string name);
    }
}
