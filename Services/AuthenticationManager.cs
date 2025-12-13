using Entites.Data_Transfer_object.User;
using Entites.Exceptions.CustomExceptions;
using Entites.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationManager: IAuthenticationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticationManager(IRepositoryManager repositoryManager,IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryManager = repositoryManager;
            _configuration=configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> LoginAsync(LoginDTO login)
        {
            var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

            if (login.UserName == null) throw new BadRequestException("Kullanıcı adı boş bırakılamaz.");
            if(login.PassWord == null) throw new BadRequestException("Şifre adı boş bırakılamaz.");
            
            var user= await _repositoryManager.authenticationRepository.GetUserAsync(login.UserName, login.PassWord);

            if (user is null)
            {
                var logFile = new LoginLog()
                {
                    girisTarihi=DateTime.UtcNow,
                    userName=login.UserName,
                    ipAdress=ip.ToString(),
                    isSuccess=false
                };
                await _repositoryManager.authenticationRepository.InsertLog(logFile);
                throw new NotFoundException("Kullanıcı bilgileri yanlış tekrar deneyiniz.");
                
            }
            else
            {
                var logFile = new LoginLog()
                {
                    girisTarihi=DateTime.UtcNow,
                    userName=login.UserName,
                    ipAdress=ip.ToString(),
                    isSuccess=true
                };
                await _repositoryManager.authenticationRepository.InsertLog(logFile);
                return await GenerateToken(user.UserName, user.RoleId);
            }
               
        }

        public async Task<string> GenerateToken(string username,int roleId )
        {
            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim("RoleId", roleId.ToString())  
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
