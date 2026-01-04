using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.User;
using Entites.Exceptions.CustomExceptions;
using Entites.Models;
using Repositories.Contracts;
using Services.Contracts;
namespace Services
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;

        public UserManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager=repositoryManager;
        }

        public async Task CreateUserAsync(CreateUserDto createUser)
        {
            var userDto = new User
            {
                UserName=createUser.userName,
                Email=createUser.Email,
                Password=createUser.Password,
                RoleId=createUser.RoleId
            };
            if (userDto is null) throw new NotFoundException("Kullanıcı bilgileri bulunamadı.");
            if (!userDto.Email.Contains("@")) throw new BadRequestException("E-posta formatı hatalı.");
            bool emailExists = await _repositoryManager.UserRepository.EmailExistsAsync(userDto.Email);
            if (emailExists) throw new BadRequestException("Mevcut e-posta sistemde kayıtlıdır.");
            bool passwordExists = await _repositoryManager.UserRepository.PassWordExistsAsync(userDto.Password);
            if (passwordExists) throw new BadRequestException("Mevcut şifre sistemde kayıtlıdır.");
            _repositoryManager.UserRepository.CreateUser(userDto);
            await _repositoryManager.saveAsyc();
        }

        public async Task<GetUserDto> getUserByIdAsync(int id)
        {

            var result = await _repositoryManager.UserRepository.GetUserByidAsync(id);
            if (result is null) throw new NotFoundException("Kullanıcı bilgileri bulunamadı.");
            var dto = new GetUserDto
            {
                Id=result.Id,
                userName=result.UserName,
                Email=result.Email,
                Password=result.Password,
                roleId=result.RoleId
            };
            return dto;
        }

        public async Task<GetUserWithRoleDto> getUsersAndRoleAsync(string username)
        {
            var result = username;
            var sonuc = await _repositoryManager.UserRepository.getUserWithRoleAsync(result.ToLower());
            if (sonuc is null) throw new NotFoundException("Kullanıcı bilgileri bulunamadı.");
            return sonuc;
        }

        public async Task<UpdateUserDto> UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            if (id < 0) throw new BadRequestException("Gelen kullanıcı bilgisi sıfırdan küçük olamaz");
            var result = await _repositoryManager.UserRepository.GetUserByidAsync(id);
            if (result is null) throw new NotFoundException("Kullanıcı bilgileri bulunamadı.");
            if (!userDto.Email.Contains("@")) throw new BadRequestException("E-posta formatı hatalı.");
            bool emailExists = await _repositoryManager.UserRepository.EmailExistsAsync(userDto.Email);
            if (emailExists) throw new BadRequestException("Mevcut e-posta sistemde kayıtlıdır.");
            bool passwordExists = await _repositoryManager.UserRepository.PassWordExistsAsync(userDto.Password);
            if (passwordExists) throw new BadRequestException("Mevcut şifre sistemde kayıtlıdır.");
            bool userExistis = await _repositoryManager.UserRepository.UsernameExistsAsync(userDto.userName);
            if (userExistis) throw new BadRequestException("Mevcut kullanıcı adı sistemde kayıtlıdır.");
            result.UserName=userDto.userName;
            result.Email=userDto.Email;
            result.Password=userDto.Password;
            result.RoleId=userDto.RoleId;
            await _repositoryManager.UserRepository.UpdateUserAsync(result);
            var updateuser = new UpdateUserDto
            {
                userName=result.UserName,
                Email=result.Email,
                Password=result.Password,
                RoleId=result.RoleId
            };
            return updateuser;
        }

        public async Task<User> UserSoftDeleteAsync(int id)
        {
            if (id < 0) throw new BadRequestException("Kullanıcı id bilgisi sıfırdan küçük olamaz");
            var result = await _repositoryManager.UserRepository.SoftDeleteAsync(id);
            return result;
        }
        public async Task<UserIzınDto> IzınEkleAsync(int id)
        {
            var user = await _repositoryManager.UserRepository.GetUserByidAsync(id);
            if (user is null) throw new NotFoundException("Kullanıcı bulunamadı.");
           
            var today = DateTime.Today;
            var toplamGun = (today - user.employmentDate.Date).TotalDays;
            var kıdemYıl = toplamGun / 365.0;
            int hakedilenIzın=0;


            if (kıdemYıl<1)
            {
                hakedilenIzın=0;
            }
            else if(kıdemYıl>=1 && kıdemYıl<=5)
            {
                hakedilenIzın=14;
            }
            else if(kıdemYıl>5 && kıdemYıl<=15)
            {
                hakedilenIzın=20;
            }
            else if(kıdemYıl>15)
            {
                hakedilenIzın=26;
            }
            if (hakedilenIzın==0) throw new BadRequestException("Kullanıcı henüz izin hakkı kazanmamıştır.");

       
            var isUpdate= await _repositoryManager.UserIzınRepository.IzınGetirAsync(id);
            if (isUpdate is null)
            {
                var result = new userIzın
                {
                    userId=id,
                    HakedilenIzın=hakedilenIzın
                };
                await _repositoryManager.UserIzınRepository.IzınEkleAsync(result);
                var dto = new UserIzınDto
                {
                    userId=result.userId,
                    HakedilenIzın=result.HakedilenIzın
                };
                return dto;
            }
            else
            {
                throw new BadRequestException("Kullanıcının izin kaydı mevcut, güncelleme bekleniyor.");
            }
        }
    }
}
