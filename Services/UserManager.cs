using Entites.Data_Transfer_object;
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
            if(userDto.UserName.Length<=5 || userDto.Password.Length<=5) throw new BadRequestException("Kullanıcı adı veya şifre karakter uzunluğu minimum beş karakterli olmalıdır.");
            bool emailExists = await _repositoryManager.UserRepository.EmailExistsAsync(userDto.Email);
            if (emailExists) throw new BadRequestException("Mevcut e-posta sistemde kayıtlıdır.");
            bool passwordExists = await _repositoryManager.UserRepository.PassWordExistsAsync(userDto.Password);
            if (passwordExists) throw new BadRequestException("Mevcut şifre sistemde kayıtlıdır.");
            _repositoryManager.UserRepository.CreateUser(userDto);
            await  _repositoryManager.saveAsyc();           
        }
    }
}
