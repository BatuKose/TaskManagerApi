using Entites.Exceptions.CustomExceptions;
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

        public async Task<userIzın> IzınGuncelleAsync(int userId, int yeniIzin)
        {
            var izinEntity = await _context.userIzıns
                .FirstOrDefaultAsync(x => x.userId == userId);

            if (izinEntity == null)
                throw new NotFoundException("İzin kaydı bulunamadı.");

            izinEntity.HakedilenIzın = yeniIzin;

            await _context.SaveChangesAsync();
            return izinEntity;
        }
        

        public async Task<UserDetayIzın>UserDetayIzinEkle(UserDetayIzın izin)
        {
            _context.UserDetayIzın.Add(izin);
            await _context.SaveChangesAsync();
            return izin;
        }
        public async Task<UserDetayIzın?> IzinDetayGetirAsync(int id)
        {
            return await _context.UserDetayIzın
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task UserDetayIzinSilAsync(UserDetayIzın izin)
        {
            _context.UserDetayIzın.Remove(izin);
            await _context.SaveChangesAsync();
        }
    }
}
