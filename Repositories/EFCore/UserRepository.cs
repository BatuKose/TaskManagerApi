using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.User;
using Entites.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class UserRepository : IUserRepository
    {
        protected readonly RepositoryContext _context;

        public UserRepository(RepositoryContext context)
        {
            _context=context;
        }

        public void CreateUser(User user )
        {

            _context.users.Add(user);
           
        }

        public async  Task<bool> EmailExistsAsync(string email)
        {
           return  await _context.users.AnyAsync(x=>x.Email == email);
        }

        public async Task<bool> PassWordExistsAsync(string password)
        {
            return await _context.users.AnyAsync(x=>x.Password==password);
        }
        public async Task<GetUserWithRoleDto> getUserWithRoleAsync(string username)
        {
            var result = await
                (
                    from u in _context.users
                    join r in _context.roles
                    on u.RoleId equals r.Id
                    where u.UserName.ToLower() == username.ToLower() && u.aktifMi==true
                    select new GetUserWithRoleDto
                    {
                        Username = u.UserName,
                        Email = u.Email,
                        RoleName = r.RoleName
                    }
                ).FirstOrDefaultAsync();

            return result;
        }

        public  async Task<User> GetUserByidAsync(int id)
        {
             var result = await _context.users.SingleOrDefaultAsync(u => u.Id==id);
            return result;
        }
    }
}
