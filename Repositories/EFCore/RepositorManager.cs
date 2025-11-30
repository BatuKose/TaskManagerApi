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
        private readonly IRoleRepository _roleRepository;
        private readonly IJobHeaderRepository _jobHeaderRepository;
        public RepositoryManager(RepositoryContext context, IUserRepository userRepository, IRoleRepository roleRepository, IJobHeaderRepository jobHeaderRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _roleRepository=roleRepository;
            _jobHeaderRepository=jobHeaderRepository;
        }

        public IUserRepository UserRepository => _userRepository;

        public IRoleRepository RoleRepository => _roleRepository;
        public IJobHeaderRepository JobHeaderRepository => _jobHeaderRepository;

        public  async Task saveAsyc()
        {
            await _context.SaveChangesAsync();
        }

    }

}
