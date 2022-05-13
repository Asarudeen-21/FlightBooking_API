using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using AdminAPIServices.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminAPIServices
{
    public class Data
    {
        public static Dictionary<string, string> Users = new Dictionary<string, string>
        {
            {"SampleUser","password" },
            {"DemoUser","password" }
        };
    }
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly InventoryContext inventoryContext;
        private readonly IConfiguration _configuration;
        public JwtTokenManager(IConfiguration configuration, InventoryContext inventoryContext)
        {
            this._configuration = configuration;
            this.inventoryContext = inventoryContext;
        }
        public string Authenticate(string userName, string password, string role)
        {
            
            //if (!Data.Users.Any(x => x.Key.Equals(userName) && x.Value.Equals(password)))
            //    return null;
            tblLoginInformation existingInfo = this.inventoryContext.LoginInfo.FirstOrDefault(a => (a.EmailID == userName) 
            && (a.Password == password) && (a.Role == role));
            
            if (existingInfo == null)
            {
                return null;
            }

            var key = _configuration.GetValue<string>("JwtConfig:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDiscriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDiscriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
