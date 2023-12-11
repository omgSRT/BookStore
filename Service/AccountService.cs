using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _repository;
        public AccountService()
        {
            _repository = new AccountRepository();
        }

        public void Add(Account account)
        {
            _repository.Add(account);
        }

        public void Delete(Account account)
        {
            _repository.Delete(account);
        }

        public IList<Account> GetAll()
        {
            return _repository.GetAll();
        }

        public Account? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Account? GetByUsernameAndPassword(string username, string password)
        {
            return _repository.GetByUsernameAndPassword(username, password);
        }

        public void Update(Account account)
        {
            _repository.Update(account);
        }
        public IList<Account> GetAllWithIncludeRoleAndStore()
        {
            return _repository.GetAllWithIncludeRoleAndStore();
        }
    }
}
