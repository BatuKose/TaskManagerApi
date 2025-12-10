using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class AuthenticationRepository: IAuthenticationRepository
    {
        protected readonly RepositoryContext _Context;
        public AuthenticationRepository(RepositoryContext repositoryContext)
        {
            _Context = repositoryContext;
        }

    }
}
