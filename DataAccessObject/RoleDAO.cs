using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class RoleDAO
    {
        private static RoleDAO _instance = null;
        private static readonly object _instanceLock = new object();
        public RoleDAO() { }

        public static RoleDAO SingletonInstance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new RoleDAO();
                    }
                    return _instance;
                }
            }
        }
        public void Add(Role role)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Roles.Find(role.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Add(role);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Delete(Role role)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Roles.Find(role.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Remove(role);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public IList<Role> GetAll()
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Role>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Role>();
            }
        }

        public Role? GetById(int id)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Role>().Find(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public void Update(Role role)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Roles.Find(role.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Update(role);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
