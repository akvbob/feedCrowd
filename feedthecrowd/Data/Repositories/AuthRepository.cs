using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FeedTheCrowd.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly FeedTheCrowdContext _context;
        private readonly IConfiguration _config;

        public AuthRepository(FeedTheCrowdContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Username.Equals(username));

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // auth succesful
            user.Token = CreateToken(user);
            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Token = CreateToken(user);

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return user;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }


        public async Task<bool> UserExists(string username)
        {
            if (await _context.User.AnyAsync(x => x.Username == username))
                return true;
            else
                return false;
        }

        public string CreateToken(User user)
        {
            var c = _config.GetSection("AppSettings:Token").Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(c);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
