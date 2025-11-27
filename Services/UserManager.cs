using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.User;
using Entites.Exceptions;
using Entites.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;

        public UserManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager=repositoryManager;
        }

        public async Task CreateUserAsync(CreateUserDto createUser)
        {
            var userDto = new User
            {
               UserName=createUser.userName,
               Email=createUser.Email,
               Password=createUser.Password,
               RoleId=createUser.RoleId              
            };
            if (userDto is null) throw new UserNotFoundException();
            if (!userDto.Email.Contains("@")) throw new BadRequestException("E-posta formatı hatalı.");
            bool emailExists = await _repositoryManager.UserRepository.EmailExistsAsync(userDto.Email);
            if (emailExists) throw new BadRequestException("Mevcut e-posta sistemde kayıtlıdır.");
            bool passwordExists = await _repositoryManager.UserRepository.PassWordExistsAsync(userDto.Password);
            if (passwordExists) throw new BadRequestException("Mevcut şifre sistemde kayıtlıdır.");
            _repositoryManager.UserRepository.CreateUser(userDto);
            await  _repositoryManager.saveAsyc();           
        }

        public async Task<GetUserDto> getUserByIdAsync(int id)
        {
            
            var result= await _repositoryManager.UserRepository.GetUserByidAsync(id);
            if (result is null) throw new UserNotFoundException();
            var dto = new GetUserDto
            {
                Id=result.Id,
                userName=result.UserName,
                Email=result.Email,
                Password=result.Password,
                roleId=result.RoleId
            };
            return dto;
        }

        public async Task<GetUserWithRoleDto> getUsersAndRoleAsync(string username)
        {
            var  result = username;
            var sonuc= await _repositoryManager.UserRepository.getUserWithRoleAsync(result.ToLower());
            if (sonuc is null) throw new UserNotFoundException();
            return sonuc;
        }

        public async Task<UpdateUserDto> UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            if (id < 0) throw new BadRequestException("Gelen kullanıcı bilgisi sıfırdan küçük olamaz");
            var result= await _repositoryManager.UserRepository.GetUserByidAsync(id);
            if (result is null) throw new UserNotFoundException();
            if (!userDto.Email.Contains("@")) throw new BadRequestException("E-posta formatı hatalı.");
            bool emailExists = await _repositoryManager.UserRepository.EmailExistsAsync(userDto.Email);
            if (emailExists) throw new BadRequestException("Mevcut e-posta sistemde kayıtlıdır.");
            bool passwordExists = await _repositoryManager.UserRepository.PassWordExistsAsync(userDto.Password);
            if (passwordExists) throw new BadRequestException("Mevcut şifre sistemde kayıtlıdır.");
            bool userExistis = await _repositoryManager.UserRepository.UsernameExistsAsync(userDto.userName);
            if (userExistis) throw new BadRequestException("Mevcut kullanıcı adı sistemde kayıtlıdır.");
            result.UserName=userDto.userName;
            result.Email=userDto.Email;
            result.Password=userDto.Password;
            result.RoleId=userDto.RoleId;
           await _repositoryManager.UserRepository.UpdateUserAsync(result);
            var updateuser = new UpdateUserDto
            {
                userName=result.UserName,
                Email=result.Email,
                Password=result.Password,
                RoleId=result.RoleId
            };
             return updateuser;
        }

        public async Task<User> UserSoftDeleteAsync(int id)
        {
            if (id < 0) throw new BadRequestException("Kullanıcı id bilgisi sıfırdan küçük olamaz");
           var result= await _repositoryManager.UserRepository.SoftDeleteAsync(id);
            return result;
        }
    }
}
