namespace NowSoft.Infrastructure.Authorization;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NowSoft.Domain.Entities;
using NowSoft.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


public interface IJwtUtils
{
    string GenerateToken(User user);
    string generateJwtToken(User user);
   int? ValidateToken(string token);
}

public class JwtUtils : IJwtUtils
{
    private readonly JwtConfig _appSettings;

    public JwtUtils(IConfiguration configuration)
    {
        //_appSettings = configuration.GetSection("AppSettings").Get<JwtConfig>();
    }

    public string GenerateToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("AdffbsdfdfKJKJdfsfsLKLJdfsdfsf32323kjklkfghlklksdkl");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public int? ValidateToken(string token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("AdffbsdfdfKJKJdfsfsLKLJdfsdfsf32323kjklkfghlklksdkl");
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // return user id from JWT token if validation successful
            return userId;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }

    //Generate Jwt Token
    public string generateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AdffbsdfdfKJKJdfsfsLKLJdfsdfsf32323kjklkfghlklksdkl"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        // Here you  can fill claim information from database for the usere as well
        var claims = new[] {
                new Claim("id", user.Id.ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, "atul"),
                new Claim(JwtRegisteredClaimNames.Email, ""),
                //new Claim("Role", "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        var token = new JwtSecurityToken("NowSoftIssuer", "nowSoftIssuer.com",
            claims,
            expires: DateTime.Now.AddMinutes(7),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}