using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.User;
using Entites.Data_Transfer_object.UserIzinDetay;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserService
    {
        public Task CreateUserAsync(CreateUserDto createUser);
        public Task<GetUserWithRoleDto> getUsersAndRoleAsync(string username);
        public Task<GetUserDto> getUserByIdAsync(int id);
        public Task<User> UserSoftDeleteAsync(int id);
        public Task<UpdateUserDto> UpdateUserAsync(int id, UpdateUserDto userDto);
        public Task<UserIzınDto> IzınEkleAsync(int id);
        public Task<UserIzinDetayEkleDTO> UserIzinDetayEkleAsync(UserIzinDetayEkleDTO dto);
        public Task<UserIzınDto> UserIzınGuncelle(int id, int izin);
        Task DeleteUserIzinAsync(int izinId);
        Task<IEnumerable<UserDetailsDTO>> UserDetailsAsync(bool? aktifMi);
        Task<IEnumerable<UserDetailsDTO>> calisanGetir();


    }
}
