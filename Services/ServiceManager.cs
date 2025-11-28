using Microsoft.AspNetCore.Identity;
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

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _userService = new Lazy<IUserService>(() => new UserManager(repositoryManager));
            _roleManager = new Lazy<IRoleManager>(() => new RoleManager(repositoryManager));
        }

        public IUserService UserService => _userService.Value;
        public IRoleManager RoleManager => _roleManager.Value;
    }

}
