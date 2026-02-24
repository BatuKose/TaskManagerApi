using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.User;
using Entites.Exceptions;
using Entites.Exceptions.CustomExceptions;
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

        public void CreateUser(User user)
        {

            _context.users.Add(user);

        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.users.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> PassWordExistsAsync(string password)
        {
            return await _context.users.AnyAsync(x => x.Password==password);
        }
        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.users.AnyAsync(x => x.UserName==username);
        }
        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.users.AnyAsync(x => x.Id == id && x.aktifMi == true);
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

        public async Task<User> GetUserByidAsync(int id)
        {
            var result = await _context.users.SingleOrDefaultAsync(u => u.Id==id && u.aktifMi==true);
            return result;
        }

        public async Task<User> SoftDeleteAsync(int id)
        {

            var user = await _context.users.FindAsync(id);
            if (user is null) throw new NotFoundException("Kullanıcı bilgileri bulunamadı.");
            if (user.aktifMi)
            {
                user.aktifMi=false;

            }
            else
            {
                user.aktifMi=true;
            }
            _context.SaveChanges();
            return user;

        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<UserDetailsDTO>> UserDetailsAsync()
        {
            var result = await _context.users
                .Where(u => u.aktifMi)
                .Select(u => new UserDetailsDTO
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    RoleId = u.RoleId,
                    RoleName = u.Role.RoleName,
                    
                })
                .ToListAsync();
            return result;
        }
    }
}
