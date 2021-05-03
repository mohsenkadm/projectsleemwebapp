using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using projectsleemwebapp.Classes;

namespace projectsleemwebapp.Classes
{
    public class JsonWebToken 
    {
        private readonly byte[] symmetricKey = Convert.FromBase64String(new Keys().SecretKey);
        public string GenerateToken(int id,string username)
        { /// crate token for log out   in one day or more 
            try
            {
               
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(symmetricKey);
                string algorithms = SecurityAlgorithms.HmacSha256Signature;

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {    //  
                    Issuer = "PBlogsSite",
                    Audience = "Subscriber",
                    NotBefore = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddDays(1),
                    Subject = new ClaimsIdentity(new[] {
                    new Claim("ID", id.ToString()),
                     new Claim("USERNAME", username),
                    new Claim("GUID", Guid.NewGuid().ToString())
                }),
                    SigningCredentials = new SigningCredentials(securityKey, algorithms)
                };
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken stoken = tokenHandler.CreateToken(tokenDescriptor);
               return tokenHandler.WriteToken(stoken);

            }
            catch(Exception ex)
            {
                return "InvalidToken";
            }
        }
        public int ValidateToken(string jwtToken)
        {
            
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidIssuer = "PBlogsSite",
                ValidAudience = "Subscriber",
                IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
            };
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken.Remove(0, 7), validationParameters, out SecurityToken ValidateToken);

            return Convert.ToInt32(principal?.FindFirst("ID")?.Value);
        }
    }
}
