using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoleService : IRoleService
    {
        private IRoleRepository _repository;
        public RoleService()
        {
            _repository = new RoleRepository();
        }

        public void Add(Role role)
        {
            _repository.Add(role);
        }

        public void Delete(Role role)
        {
            _repository.Delete(role);
        }

        public IList<Role> GetAll()
        {
            return _repository.GetAll();
        }

        public Role? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Role role)
        {
            _repository.Update(role);
        }
    }
}
