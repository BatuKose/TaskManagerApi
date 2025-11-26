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
    }
}
