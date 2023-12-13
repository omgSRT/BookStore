using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        private AccountDAO _dao;
        public AccountRepository()
        {
            _dao = new AccountDAO();
        }

        public void Add(Account account)
        {
            _dao.Add(account);
        }

        public void Delete(Account account)
        {
            _dao.Delete(account);
        }

        public IList<Account> GetAll()
        {
            return _dao.GetAll();
        }

        public Account? GetById(int id)
        {
            return _dao.GetById(id);
        }

        public Account? GetByUsernameAndPassword(string username, string password)
        {
            return _dao.GetByUsernameAndPassword(username, password);
        }

        public void Update(Account account)
        {
            _dao.Update(account);
        }
        public IList<Account> GetAllWithIncludeRoleAndStore()
        {
            return _dao.GetAllWithIncludeRoleAndStore();
        }

        public List<Account> GetByName(string name)
        {
            return _dao.GetByName(name);
        }
    }
}
