using Entites.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class userIzınRepository: IUserIzınRepository
    {
        protected readonly RepositoryContext _context;

        public userIzınRepository(RepositoryContext context)
        {
            _context=context;
        }

        public async Task<userIzın> IzınEkleAsync(userIzın izin)
        {
            _context.userIzıns.Add(izin);
            await _context.SaveChangesAsync();
            return izin;
        }

        public async Task<userIzın> IzınGetirAsync(int userId)
        {
            var izin= await _context.userIzıns.FirstOrDefaultAsync(x => x.userId==userId);
            return izin;
        }

        public Task<userIzın> IzınGuncelleAsync(userIzın izin)
        {
            throw new NotImplementedException();
        }
    }
}
