using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoleRepository : IRoleRepository
    {
        public void Add(Role role)
        {
            RoleDAO.SingletonInstance.Add(role);
        }

        public void Delete(Role role)
        {
            RoleDAO.SingletonInstance.Delete(role);
        }

        public IList<Role> GetAll()
        {
            return RoleDAO.SingletonInstance.GetAll();
        }

        public Role? GetById(int id)
        {
            return RoleDAO.SingletonInstance.GetById(id);
        }

        public void Update(Role role)
        {
            RoleDAO.SingletonInstance.Update(role);
        }
    }
}
