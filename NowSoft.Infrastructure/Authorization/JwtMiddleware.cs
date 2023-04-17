using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NowSoft.Application.Contracts;
using NowSoft.Domain.Entities;
using NowSoft.Sql.Queries;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Dapper;

namespace NowSoft.Infrastructure.Authorization;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration configuration;

    public JwtMiddleware(RequestDelegate next, IConfiguration _configuration)
    {
        _next = next;
        this.configuration = _configuration;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
            //Validate the token
            attachUserToContext(context, token);
        await _next(context);
    }
    private void attachUserToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AdffbsdfdfKJKJdfsfsLKLJdfsdfsf32323kjklkfghlklksdkl"));
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = key,
                ValidIssuer = "NowSoftIssuer",
                ValidAudience = "nowSoftIssuer.com",
                // set clockskew to zero so tokens expire exactly at token expiration time.
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            // attach user to context on successful jwt validation
            context.Items["User"] =  GetById(userId);
        }
        catch (Exception ex)
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
        }
    }
    private User GetById(int UserId)
    {
        try
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                User userReq = new User
                {
                    Id = UserId,

                };
                var user =  connection.QuerySingleOrDefault<User>(UserQueries.UserById, userReq);
                if (user != null)
                {
                    return user;
                }
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

}