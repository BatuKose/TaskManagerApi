using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{

    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly IUserRepository _userRepository;

        public RepositoryManager(RepositoryContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public IUserRepository UserRepository => _userRepository;

        public  async Task saveAsyc()
        {
            await _context.SaveChangesAsync();
        }

    }

}
