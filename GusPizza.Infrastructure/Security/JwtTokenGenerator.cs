using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GusPizza.Infrastructure.Security;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid id, string username, string role);
}

public class JwtTokenGenerator(IConfiguration config) : IJwtTokenGenerator
{
    private readonly IConfiguration configuration = config;
    public string GenerateToken(Guid id, string username, string role)
    {
        var claims = new[]{
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var jwtKey = configuration["Jwt:key"] ?? throw new ArgumentNullException("Jwt:key", "JWT key cannot be null");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(23),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
