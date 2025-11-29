using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRoleRepository
    {
        Task<Role> InsertRoleAsync(Role role);
        Task<Role> GetRoleByİdAsync(int id);
        Task<Role>DeleteRoleAsync(Role role);
        Task<Role>UpdateRoleAsync(Role role);
        Task<bool> RoleExistsAsync(string name);
    }
}
