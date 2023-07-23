using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Helper
{
    public class JWTService
    {
        private string securekey = "qwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwasdjhkhadsgjhdsgajhsadgvadsghcvcgdasghfsdagfv"; 
        public string Generate(int id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securekey)); 

            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(credential);

            var payload = new JwtPayload(id.ToString (),null,null,null,DateTime.Today.AddDays(1));

            var securityToken = new JwtSecurityToken(header,payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key =Encoding .ASCII.GetBytes(securekey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey =new SymmetricSecurityKey (key),
                ValidateIssuerSigningKey =true ,
                ValidateAudience =false ,
                ValidateIssuer =false ,
            },out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
        }
    }
}

    

