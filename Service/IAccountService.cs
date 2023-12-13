using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IAccountService
    {
        void Add(Account account);
        void Update(Account account);
        void Delete(Account account);
        IList<Account> GetAll();
        Account? GetById(int id);
        Account? GetByUsernameAndPassword(String username, String password);
        IList<Account> GetAllWithIncludeRoleAndStore();
        List<Account> GetByName(string name);
    }
}
