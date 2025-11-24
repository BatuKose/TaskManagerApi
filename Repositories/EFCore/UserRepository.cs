using Entites.Data_Transfer_object;
using Entites.Models;
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
        public void CreateUser(User user )
        {

            _context.users.Add(user);
           
        }
    }
}
