using Microsoft.AspNetCore.Mvc;
using NowSoft.Application.Contracts;
using NowSoft.Application.DTOs;
using NowSoft.Infrastructure.Authorization;
using NowSoft.Common.Response;
using NowSoft.Domain.Entities;

namespace NowSoft.Api.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<BaseResponse<string>> Signup(RegistrationDto registrationDto)
        {
            return await _unitOfWork.Account.RegisterAsync(registrationDto);
        }
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<BaseResponse<string>> Login(AuthenticationDto loginDto)
        {
            return await _unitOfWork.Account.LoginAsync(loginDto);
        }
        [HttpGet("auth/balance")]
        [Authorize]
        public async Task<BaseResponse<decimal>> GetBalance(int userId)
        {
            //var user = GetUserId();
            return await _unitOfWork.Account.GetBalance(userId);
        }
        protected int GetUserId()
        {
            //return int.Parse(_httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "UserId").Value);  
            return int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
        }
    }
}