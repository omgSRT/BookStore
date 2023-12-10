using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IRoleService
    {
        void Add(Role role);
        void Update(Role role);
        void Delete(Role role);
        IList<Role> GetAll();
        Role? GetById(int id);
    }
}
