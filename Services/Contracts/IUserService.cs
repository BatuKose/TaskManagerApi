using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.User;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserService
    {
        public Task CreateUserAsync(CreateUserDto createUser);
        public Task<GetUserWithRoleDto> getUsersAndRoleAsync(string username);
        public Task<GetUserDto> getUserByIdAsync(int id);
        public Task<User> UserSoftDeleteAsync(int id);
        public Task<UpdateUserDto> UpdateUserAsync(int id, UpdateUserDto userDto);
        
    }
}
