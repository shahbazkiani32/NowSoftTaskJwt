
using Dapper;
using Microsoft.Extensions.Configuration;
using NowSoft.Application.Contracts;
using NowSoft.Application.DTOs;
using NowSoft.Infrastructure.Authorization;
using NowSoft.Common.Response;
using NowSoft.Domain.Entities;
using NowSoft.Sql.Queries;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace NowSoft.Infrastructure.Service
{
    public class AccountService : IAccountService
    {
        #region ===[ Private Members ]=============================================================

        private readonly IConfiguration configuration;
        private IJwtUtils _jwtUtils;

        #endregion

        #region ===[ Constructor ]=================================================================

        public AccountService(IConfiguration configuration, IJwtUtils jwtUtils)
        {
            this.configuration = configuration;
            _jwtUtils = jwtUtils;
        }

        #endregion

        #region ===[ IAccountService Methods ]==================================================

        public async Task<BaseResponse<string>> LoginAsync(AuthenticationDto authDto)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
                {
                    connection.Open();
                    var user = await connection.QuerySingleOrDefaultAsync<User>(UserQueries.UserLogin, authDto);
                    if (user != null)
                    {
                        if(user.BalancAmount==null)
                        {
                            user.BalancAmount = 5;
                            await connection.ExecuteAsync(UserQueries.UpdateUserBalance, user);
                        }
                        //string token = _jwtUtils.GenerateToken(user);
                        string token = _jwtUtils.generateJwtToken(user);
                        return new BaseResponse<string>() { StatusCode = HttpStatusCode.OK, Token = token, IsSuccessful = true, Result = "Login Successful" };
                    }
                    return new BaseResponse<string>() { StatusCode = HttpStatusCode.NotFound, IsSuccessful = true, Result = null, Message = "Incorrect Username or Password" };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>() { StatusCode = HttpStatusCode.InternalServerError, IsSuccessful = false, Result = null, Message = "Internal Server Error" };
            }
        }

        public async Task<BaseResponse<string>> RegisterAsync(RegistrationDto registrationDto)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(UserQueries.AddUser, registrationDto);

                    return new BaseResponse<string>() { StatusCode = HttpStatusCode.OK, IsSuccessful = true, Result = result.ToString() };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>() { StatusCode = HttpStatusCode.InternalServerError, IsSuccessful = false, Result = null, Message = "Internal Server Error" };
            }
        }

        public Task<BaseResponse<RefreshTokenDto>> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<object>> LogoutAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<object>> ChangePassword(string email, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task AddRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<decimal>> GetBalance(int UserId)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
                {

                    connection.Open();
                    User userReq = new User
                    {
                        Id = UserId
                    };
                    var balance = await connection.QuerySingleOrDefaultAsync<decimal>(UserQueries.GetBalance, userReq);
                    if (balance != null)
                    {
                        return new BaseResponse<decimal>() { StatusCode = HttpStatusCode.OK, IsSuccessful = true, Result = balance };
                    }
                    return new BaseResponse<decimal>() { StatusCode = HttpStatusCode.NotFound, IsSuccessful = true,Message="User not Found" };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<decimal>() { StatusCode = HttpStatusCode.InternalServerError, IsSuccessful = false, Message = "Internal Server Error" };
            }
        }

        #endregion
    }
}
