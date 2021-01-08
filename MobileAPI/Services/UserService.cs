using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MobileAPI.Data;
using MobileAPI.Helpers;
using MobileAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MobileAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly BoodschapContext _boodschapContext;

        public UserService(IOptions<AppSettings> appSettings, BoodschapContext boodschapContext)
        {
            _appSettings = appSettings.Value;
            _boodschapContext = boodschapContext;
        }

        public User Authenticate(string email, string password)
        {
            var user = _boodschapContext.Users.SingleOrDefault(x => x.Email == email);

            string hashed = Hashing.getHash(password, user.HashSalt);

            if (user == null || user.Password != hashed)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.UserID.ToString()),
                    new Claim("Email", user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;
            return user;
        }
    }
}
