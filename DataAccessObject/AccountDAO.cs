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
        private static AccountDAO _instance = null;
        private static readonly object _instanceLock = new object();
        public AccountDAO() { }

        public static AccountDAO SingletonInstance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new AccountDAO();
                    }
                    return _instance;
                }
            }
        }
        public void Add(Account account)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Accounts.Find(account.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Add(account);
                    _context.SaveChanges();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Accounts.Find(account.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Remove(account);
                    _context.SaveChanges();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Account>().ToList();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Account>()
                        .Include(a => a.Role)
                        .Include(a => a.Store)
                        .FirstOrDefault(a => a.Id == id);
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Accounts.Find(account.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Update(account);
                    _context.SaveChanges();
                }
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
                using (var _context = new BookStoreDBContext())
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
                using (var _context = new BookStoreDBContext())
                {
                    var list = _context.Set<Account>()
                    .Include("Role")
                    .Include("Store")
                    .ToList();
                    return list;
                }
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
                using (var _context = new BookStoreDBContext()) { 
                    return _context.Accounts.Where(p => p.Name.Contains(name)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
