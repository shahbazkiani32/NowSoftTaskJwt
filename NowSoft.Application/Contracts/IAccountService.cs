using NowSoft.Domain.Entities;

namespace NowSoft.Application.Contracts;

public interface IAccountService
{
	Task<BaseResponse<string>> LoginAsync(AuthenticationDto authDto);


    Task<BaseResponse<string>> RegisterAsync(RegistrationDto registrationDto);

	Task<BaseResponse<decimal>> GetBalance(int UserId);

    Task<BaseResponse<RefreshTokenDto>> RefreshToken(RefreshTokenDto refreshTokenDto);

	Task<BaseResponse<object>> LogoutAsync(string token);

	Task<BaseResponse<object>> ChangePassword(string email, string currentPassword, string newPassword);

	Task AddRole(string roleName);
}