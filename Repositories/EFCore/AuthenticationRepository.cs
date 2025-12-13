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
    public class AuthenticationRepository: IAuthenticationRepository
    {
        protected readonly RepositoryContext _Context;
        public AuthenticationRepository(RepositoryContext repositoryContext)
        {
            _Context = repositoryContext;
        }

        public async Task<User?> GetUserAsync(string username, string password)
        {
           return await _Context.users.FirstOrDefaultAsync(x=>x.UserName == username && x.Password == password);
        }

        public async Task<LoginLog> InsertLog(LoginLog loginLog)
        {
             _Context.LoginLogs.AddAsync(loginLog);
            await _Context.SaveChangesAsync();
            return loginLog;
        }
    }
}
