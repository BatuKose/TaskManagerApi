using Entites.Data_Transfer_object.Role;
using Entites.Exceptions;
using Entites.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoleManager : IRoleManager
    {
        private readonly IRepositoryManager _Manager;

        public RoleManager(IRepositoryManager repositoryManager)
        {
            _Manager=repositoryManager;
        }

        public async Task<Role> DeleteRoleAsync(int id)
        {
           if(id<=0) throw new ArgumentNullException("id");
           var result= await _Manager.RoleRepository.GetRoleByİdAsync(id);
           if(result==null) throw new RoleNotFoundException();
           if (result.RoleName=="admin" || result.RoleName=="ADMIN") throw new BadRequestException("Admin rolünü silemezsiniz");
            var deletedRole = await _Manager.RoleRepository.DeleteRoleAsync(result);
            return deletedRole;

        }

        public async Task<GetRoleDto> GetRoleByİdAsync( int id)
        {
            if(id<=0) throw new ArgumentNullException("id");
            var result = await _Manager.RoleRepository.GetRoleByİdAsync(id);
            if(result is null) throw new RoleNotFoundException();
            var dto = new GetRoleDto()
            {
                RoleName = result.RoleName,
                Id = result.Id
            };
            return dto;
        }

        public async Task<InsertRoleDTO> InsertRoleAsync(InsertRoleDTO roleDTO)
        {
            if (roleDTO == null) throw new RoleNotFoundException();

            if (roleDTO.RoleName.Contains("admin",StringComparison.OrdinalIgnoreCase)) throw new BadRequestException("Admin rolü kullanıcılar tarafından eklenemez");
            var result = new Role()
            {
                RoleName = roleDTO.RoleName,
            };
           await _Manager.RoleRepository.InsertRoleAsync(result);
            return new InsertRoleDTO
            {
                RoleName=roleDTO.RoleName,
            };
        }
    }
}
