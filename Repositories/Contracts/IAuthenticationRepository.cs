using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IAuthenticationRepository
    {
        Task<User?>GetUserAsync(string username, string password);
    }
}
