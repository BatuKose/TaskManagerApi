using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.User;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IUserRepository
    {

        void CreateUser(User user);
        Task <bool> EmailExistsAsync(string email);
        Task<bool> PassWordExistsAsync(string password);
        Task<bool> UsernameExistsAsync(string username);
        Task<GetUserWithRoleDto> getUserWithRoleAsync(string username);
        Task<User> GetUserByidAsync(int id);
        Task<User> SoftDeleteAsync(int id);
        Task<User>UpdateUserAsync(User user);
        Task<bool> UserExistsAsync(int id);
    }
}
