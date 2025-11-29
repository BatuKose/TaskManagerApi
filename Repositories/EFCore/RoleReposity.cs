using Entites.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RoleReposity : IRoleRepository
    {
        protected readonly RepositoryContext _Context;
        public RoleReposity(RepositoryContext repositoryContext)
        {
            _Context = repositoryContext;
        }

        public  async Task<Role> DeleteRoleAsync(Role role)
        {
                _Context.roles.Remove(role);
                await _Context.SaveChangesAsync();
                return role;
        }

        public async Task<Role> GetRoleByİdAsync(int id)
        {
            return await _Context.roles.SingleOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Role> InsertRoleAsync(Role role)
        {
            _Context.roles.Add(role);
             await _Context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> RoleExistsAsync(string Role)
        {
          return await _Context.roles.AnyAsync(x => x.RoleName.Equals(Role));
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
           _Context.roles.Update(role);
            await _Context.SaveChangesAsync();
            return role;
        }
    }
}
