using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRoleRepository
    {
        void Add(Role role);
        void Update(Role role);
        void Delete(Role role);
        IEnumerable<Role> GetAll();
        Role? GetById(int id);
    }
}
