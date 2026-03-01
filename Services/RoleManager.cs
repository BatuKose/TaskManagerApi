using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Entites.Data_Transfer_object.Role;
using Entites.Exceptions.CustomExceptions;
using Entites.Models;
using Repositories.Contracts;
using Services.Contracts;


namespace Services
{
    public class RoleManager : IRoleManager
    {
        private readonly IRepositoryManager _Manager;

        public RoleManager(IRepositoryManager repositoryManager)
        {
            _Manager=repositoryManager;
        }

        //public async Task<Role> DeleteRoleAsync(int id)
        //{
        //   if(id<=0) throw new ArgumentNullException("id");
        //   var result= await _Manager.RoleRepository.GetRoleByİdAsync(id);
        //   if(result==null) throw new ();
        //   if (result.RoleName.Contains("admin",StringComparison.OrdinalIgnoreCase)) throw new BadRequestException("Admin rolünü silemezsiniz");
        //    var deletedRole = await _Manager.RoleRepository.DeleteRoleAsync(result);
        //    return deletedRole;

        //}
        public async Task<Role> DeleteRoleAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Geçersiz id");

            var result = await _Manager.RoleRepository.GetRoleByİdAsync(id);

            if (result == null)
                throw new NotFoundException("Rol bulunamadı");

            if (result.RoleName.Contains("admin", StringComparison.OrdinalIgnoreCase))
                throw new BadRequestException("Admin rolünü silemezsiniz");

            var deletedRole = await _Manager.RoleRepository.DeleteRoleAsync(result);
            return deletedRole;
        }

        public async Task<GetRoleDto> GetRoleByİdAsync( int id)
        {
            if(id<=0) throw new NotFoundException("id");
            var result = await _Manager.RoleRepository.GetRoleByİdAsync(id);
            if (result is null) throw new NotFoundException("Rol bilgileri bulunamadı");
            var dto = new GetRoleDto()
            {
                RoleName = result.RoleName,
                Id = result.Id
            };
            return dto;
        }

        public async Task<InsertOrUpdateRoleDTO> InsertRoleAsync(InsertOrUpdateRoleDTO roleDTO)
        {
            if (roleDTO == null) throw new NotFoundException("Rol bilgileri bulunamadı");
            if (roleDTO.RoleName.Contains("admin",StringComparison.OrdinalIgnoreCase)) throw new BadRequestException("Admin rolü kullanıcılar tarafından eklenemez");
            var result = new Role()
            {
                RoleName = roleDTO.RoleName,
            };
           await _Manager.RoleRepository.InsertRoleAsync(result);
            return new InsertOrUpdateRoleDTO
            {
                RoleName=roleDTO.RoleName,
            };
        }

        public async Task<InsertOrUpdateRoleDTO> UpdateRoleAsync(int id, InsertOrUpdateRoleDTO dto)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));
            if (dto.RoleName.Contains("admin", StringComparison.OrdinalIgnoreCase))
                throw new BadRequestException("Admin rolu adında güncelleme yapılmaz");
            var role = await _Manager.RoleRepository.GetRoleByİdAsync(id);
            if (role == null) throw new NotFoundException("Rol bilgileri bulunamadı");
            if (role.RoleName.Contains("admin", StringComparison.OrdinalIgnoreCase))
                throw new BadRequestException("Admin rolünü adı altında güncelleyemezsin.");
            var RoleExists = await _Manager.RoleRepository.RoleExistsAsync(dto.RoleName);
            if (RoleExists) throw new BadRequestException("aynı rol isimde güncelleme yapılmaz");
            role.RoleName = dto.RoleName;
            await _Manager.RoleRepository.UpdateRoleAsync(role);
            return new InsertOrUpdateRoleDTO
            {
                RoleName = role.RoleName
            };
        }
        public async Task<List<GetRoleDto>> GetRolesAsync()
        {
            var roles = await _Manager.RoleRepository.GetRolesAsync();
            if(roles == null || roles.Count == 0) throw new NotFoundException("Rol bilgileri bulunamadı");
            var roleDtos = roles.Select(role => new GetRoleDto
            {
                Id = role.Id,
                RoleName = role.RoleName
            }).ToList();
            return roleDtos;
        }
    }
}
