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
        private readonly IjobDetailRepository _jobDetailRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IUserIzınRepository _userIzınRepository;
        public RepositoryManager(RepositoryContext context, IUserRepository userRepository, 
            IRoleRepository roleRepository, IJobHeaderRepository jobHeaderRepository,
            IjobDetailRepository jobDetailRepository,IAuthenticationRepository authenticationRepository
           , IUserIzınRepository userIzınRepository
            )
        {
            _context = context;
            _userRepository = userRepository;
            _roleRepository=roleRepository;
            _jobHeaderRepository=jobHeaderRepository;
            _jobDetailRepository=jobDetailRepository;
            _authenticationRepository=authenticationRepository;
            _userIzınRepository=userIzınRepository;
        }

        public IUserRepository UserRepository => _userRepository;

        public IRoleRepository RoleRepository => _roleRepository;
        public IJobHeaderRepository JobHeaderRepository => _jobHeaderRepository;
        public IjobDetailRepository JobDetailRepository => _jobDetailRepository;
        public IAuthenticationRepository authenticationRepository => _authenticationRepository;
        public IUserIzınRepository UserIzınRepository => _userIzınRepository;

        public  async Task saveAsyc()
        {
            await _context.SaveChangesAsync();
        }

    }

}
