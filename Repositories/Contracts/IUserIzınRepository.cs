using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IUserIzınRepository
    {
        Task<userIzın> IzınEkleAsync(userIzın izin);
        Task<userIzın> IzınGuncelleAsync(userIzın izin);
        Task<userIzın> IzınGetirAsync(int userId);
    }
}
