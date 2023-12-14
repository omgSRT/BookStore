using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        public void Add(Account account)
        {
            AccountDAO.SingletonInstance.Add(account);
        }

        public void Delete(Account account)
        {
            AccountDAO.SingletonInstance.Delete(account);
        }

        public IList<Account> GetAll()
        {
            return AccountDAO.SingletonInstance.GetAll();
        }

        public Account? GetById(int id)
        {
            return AccountDAO.SingletonInstance.GetById(id);
        }

        public Account? GetByUsernameAndPassword(string username, string password)
        {
            return AccountDAO.SingletonInstance.GetByUsernameAndPassword(username, password);
        }

        public void Update(Account account)
        {
            AccountDAO.SingletonInstance.Update(account);
        }
        public IList<Account> GetAllWithIncludeRoleAndStore()
        {
            return AccountDAO.SingletonInstance.GetAllWithIncludeRoleAndStore();
        }

        public List<Account> GetByName(string name)
        {
            return AccountDAO.SingletonInstance.GetByName(name);
        }
    }
}
