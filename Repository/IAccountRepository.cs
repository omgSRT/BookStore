using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAccountRepository
    {
        void Add(Account account);
        void Update(Account account);
        void Delete(Account account);
        IEnumerable<Account> GetAll();
        Account? GetById(int id);
        Account? GetByUsernameAndPassword(String username, String password);
    }
}
