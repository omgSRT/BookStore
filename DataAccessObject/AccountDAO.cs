using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class AccountDAO
    {
        private BookStoreDBContext _context;
        public AccountDAO()
        {
            _context = new BookStoreDBContext();
        }
        public void Add(Account account)
        {
            try
            {
                var checkExist = _context.Accounts.Find(account.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Add(account);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Delete(Account account)
        {
            try
            {
                var checkExist = _context.Accounts.Find(account.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Remove(account);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public IList<Account> GetAll()
        {
            try
            {
                return _context.Set<Account>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Account>();
            }
        }

        public Account? GetById(int id)
        {
            try
            {
                return _context.Set<Account>().Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public void Update(Account account)
        {
            try
            {
                var checkExist = _context.Accounts.Find(account.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Update(account);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public Account? GetByUsernameAndPassword(String username, String password)
        {
            try
            {
                var existedAccount = _context.Set<Account>().Where(x => x.Username != null && x.Username!.Equals(username)).FirstOrDefault();
                if (existedAccount != null)
                {
                    if (existedAccount.Password is null || !existedAccount.Password.Equals(password))
                    {
                        return null;
                    }
                    return existedAccount;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public IList<Account> GetAllWithIncludeRoleAndStore()
        {
            try
            {
                var list = _context.Set<Account>()
                    .Include("Role")
                    .Include("Store")
                    .ToList();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Account>();
            }
        }
        public List<Account> GetByName(string name)
        {
            try
            {
                return _context.Accounts.Where(p => p.Name.Contains(name)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }



    }
}
