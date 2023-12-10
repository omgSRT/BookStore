using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBookRepository
    {
        void Add(Book book);
        void Delete(Book book);
        void Update(Book book);
        IEnumerable<Book> GetAll();
        Book? GetById(int id);
    }
}
