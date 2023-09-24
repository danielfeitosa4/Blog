using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Models;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Services
{
    public class TokenService
    {
        public string GenerateToken(User user)
        {
            // MANIPULADOR DE TOKEN PARA GERAR O TOKEN
            var tokenHandler = new JwtSecurityTokenHandler();

            // CHAVE QUE ESTA NA NOSSA CONFIGURATION, POREM O "TOKENHANDLER" ESPERA UM ARRAY DE BYTES, POR ISSO PRECISO CONVERTER
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

            // CONFIGURACOES DO TOKEN, ESSE ITEM DE FATO VAI CONTER TODAS AS CONFIGURACOES DO TOKEN
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "daniel"), // User.Identity.Name
                    new Claim(ClaimTypes.Role, "admin") // User.IsInRole
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            // CRIACAO DO TOKEN
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // GERA UMA STRING BASEADA NO TOKEN
            return tokenHandler.WriteToken(token);
        }
    }
}