using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IRoleManager> _roleManager;
        private readonly Lazy<IJobHeaderService> _jobHeaderManager;
        private readonly Lazy<IJobDetailService> _jobDetailManager;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IHttpContextAccessor> _httpContextAccessor;
        private readonly Lazy<IZimmetDemirbasService> _zimmetDemirbasService;


        public ServiceManager(IRepositoryManager repositoryManager,IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userService = new Lazy<IUserService>(() => new UserManager(repositoryManager));
            _roleManager = new Lazy<IRoleManager>(() => new RoleManager(repositoryManager));
            _jobHeaderManager = new Lazy<IJobHeaderService>(() => new JobHeaderManager(repositoryManager));
            _jobDetailManager = new Lazy<IJobDetailService>(() => new JobDetailManager(repositoryManager));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(repositoryManager, configuration, httpContextAccessor));
            _zimmetDemirbasService = new Lazy<IZimmetDemirbasService>(() => new ZimmetDemirbasManager(repositoryManager));
        }
        public IUserService UserService => _userService.Value;
        public IRoleManager RoleManager => _roleManager.Value;

        public IJobHeaderService JobHeaderService => _jobHeaderManager.Value;
        public IJobDetailService JobDetailService => _jobDetailManager.Value;
        public IAuthenticationService authenticationService => _authenticationService.Value;

       public IZimmetDemirbasService ZimmetDemirbasService => _zimmetDemirbasService.Value;
    }

}
