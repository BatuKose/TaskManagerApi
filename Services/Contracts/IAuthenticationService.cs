using Entites.Data_Transfer_object.User;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<string>LoginAsync(LoginDTO login);
    }
}
