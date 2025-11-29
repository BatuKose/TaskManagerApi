using Entites.Data_Transfer_object.Role;
using Entites.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IRoleManager
    {
        Task<InsertOrUpdateRoleDTO> InsertRoleAsync(InsertOrUpdateRoleDTO roleDTO);
        Task<GetRoleDto> GetRoleByİdAsync(int id);
        Task<Role> DeleteRoleAsync(int id);
        Task<InsertOrUpdateRoleDTO> UpdateRoleAsync(int id,InsertOrUpdateRoleDTO dto);
    }
}
