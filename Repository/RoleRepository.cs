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
        private RoleDAO _dao;
        public RoleRepository()
        {
            _dao = new RoleDAO();
        }

        public void Add(Role role)
        {
            _dao.Add(role);
        }

        public void Delete(Role role)
        {
            _dao.Delete(role);
        }

        public IList<Role> GetAll()
        {
            return _dao.GetAll();
        }

        public Role? GetById(int id)
        {
            return _dao.GetById(id);
        }

        public void Update(Role role)
        {
            _dao.Update(role);
        }
    }
}
