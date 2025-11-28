using Entites.Data_Transfer_object.Role;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IRoleManager
    {
        Task<InsertRoleDTO>InsertRoleAsync(InsertRoleDTO roleDTO);
        Task<GetRoleDto> GetRoleByİdAsync(int id);
        Task<Role> DeleteRoleAsync(int id);
    }
}
