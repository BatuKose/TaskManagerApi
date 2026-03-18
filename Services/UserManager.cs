using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.User;
using Entites.Data_Transfer_object.UserIzinDetay;
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
        public async Task<UserIzınDto> UserIzınGuncelle(int id, int izin)
        {
            var updated = await _repositoryManager
                .UserIzınRepository
                .IzınGuncelleAsync(id, izin);

            return new UserIzınDto
            {
                userId = updated.userId,
                HakedilenIzın = updated.HakedilenIzın
            };
        }

        public async Task<UserIzinDetayEkleDTO> UserIzinDetayEkleAsync(UserIzinDetayEkleDTO user)
        {
            var userExists = await _repositoryManager.UserRepository.UserExistsAsync(user.UserId);
            if (!userExists) throw new NotFoundException("Kullanıcı bulunamadı.");
            if(user.BaslangicTarihi<DateTime.Today) throw new BadRequestException("İzin başlangıç tarihi bugünden önce olamaz.");
            if(user.BitisTarihi<user.BaslangicTarihi) throw new BadRequestException("İzin bitiş tarihi, başlangıç tarihinden önce olamaz.");
            var izinHakkiVarMi = await _repositoryManager.UserIzınRepository.IzınGetirAsync(user.UserId);
            if (izinHakkiVarMi.HakedilenIzın<=0) throw new BadRequestException("Kullanıcının yeterli izin hakkı bulunmamaktadır.");
            int IzinliGunSayisi = (user.BitisTarihi - user.BaslangicTarihi).Days + 1;
            if(IzinliGunSayisi>izinHakkiVarMi.HakedilenIzın) throw new BadRequestException("Kullanıcının talep ettiği izin günü, kalan izin hakkından fazladır.");
            int KalaIzin=izinHakkiVarMi.HakedilenIzın- IzinliGunSayisi;
            await  _repositoryManager.UserIzınRepository.IzınGuncelleAsync(user.UserId, KalaIzin);
            var izinDetay = new UserDetayIzın
            {
                UserId=user.UserId,
                BaslangicTarihi=user.BaslangicTarihi,
                BitisTarihi=user.BitisTarihi,
                IzınDetay=user.IzınDetay,
                YoneticiOnay=false
            };
            await _repositoryManager.UserIzınRepository.UserDetayIzinEkle(izinDetay);
            var ReturnDto = new UserIzinDetayEkleDTO
            {
                UserId=izinDetay.UserId,
                BaslangicTarihi=izinDetay.BaslangicTarihi,
                BitisTarihi=izinDetay.BitisTarihi,
                IzınDetay=izinDetay.IzınDetay,
                YoneticiOnay=izinDetay.YoneticiOnay
            };
            return ReturnDto;
        }

        public async Task DeleteUserIzinAsync(int izinId)
        {
            var izin = await _repositoryManager.UserIzınRepository.IzinDetayGetirAsync(izinId);
            if (izin is null) throw new NotFoundException("Kullanıcının izin bilgisi bulunamadı.");

            if (izin.YoneticiOnay) throw new BadRequestException("Onaylanmış izin bilgisi silinemez.");
            var mevcutIzın= await _repositoryManager.UserIzınRepository.IzınGetirAsync(izin.UserId);
            var eklenecekGunSayısı = (izin.BitisTarihi - izin.BaslangicTarihi).Days + 1;
            int ToplamHakedis=mevcutIzın.HakedilenIzın+ eklenecekGunSayısı;
            var userId= izin.UserId;
            await _repositoryManager.UserIzınRepository.IzınGuncelleAsync(userId, ToplamHakedis);
            await _repositoryManager.UserIzınRepository.UserDetayIzinSilAsync(izin);
        }
        public async Task<IEnumerable<UserDetailsDTO>> UserDetailsAsync(bool? aktifMi)
        {
            var result = await _repositoryManager
                .UserRepository
                .UserDetailsAsync(aktifMi);

            if (!result.Any())
                throw new NotFoundException("Kullanıcı bilgileri bulunamadı.");

            return result;
        }
        public async Task<IEnumerable<UserDetailsDTO>> calisanGetir()
        {
            var result = await _repositoryManager
                .UserRepository.CalisanlarıGetir();      

            if (!result.Any())
                throw new NotFoundException("Çalışan bilgileri bulunamadı.");

            return result;
        }
        public async Task<IEnumerable<UserDetailsDTO>> yoneticGetir()
        {
            var result = await _repositoryManager
                .UserRepository.yoneticiGetir();

            if (!result.Any())
                throw new NotFoundException("yönetici bilgileri bulunamadı.");

            return result;
        }
    }
}
