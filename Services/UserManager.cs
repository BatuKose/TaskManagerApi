using Entites.Data_Transfer_object;
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
               RoleId=createUser.RoleId,
            
            };


            _repositoryManager.UserRepository.CreateUser(userDto);
            await  _repositoryManager.saveAsyc();
            
        }
    }
}
