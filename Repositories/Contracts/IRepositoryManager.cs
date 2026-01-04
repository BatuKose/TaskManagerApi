using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager 
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IJobHeaderRepository JobHeaderRepository { get; }
        IjobDetailRepository JobDetailRepository { get; }
        IAuthenticationRepository authenticationRepository { get; }
        IUserIzınRepository UserIzınRepository { get; }
        Task saveAsyc();
    }
}
