using CatalogoFilmes.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatalogoFilmes.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _config;

        public JwtHelper(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Role, usuario.Role)
            };

            var secret = _config["Jwt:Secret"] ?? throw new InvalidOperationException("Configuração Jwt:Secret não encontrada.");
            var issuer = _config["Jwt:Issuer"] ?? throw new InvalidOperationException("Configuração Jwt:Issuer não encontrada.");
            var audience = _config["Jwt:Audience"] ?? throw new InvalidOperationException("Configuração Jwt:Audience não encontrada.");


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
         
        public static string getUserIdToken(ClaimsIdentity identity)
        {
            var idUsuario = identity.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Single().Value;
            return idUsuario;
        }
    }

}
