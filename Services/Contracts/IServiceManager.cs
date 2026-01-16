using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IRoleManager RoleManager { get; }
        IJobHeaderService JobHeaderService { get; }
        IJobDetailService JobDetailService { get; }
        IAuthenticationService authenticationService { get; }
        IZimmetDemirbasService ZimmetDemirbasService { get; }

    }
}
