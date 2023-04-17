namespace NowSoft.Application.DTOs;

public class LoginDto
{
	public string? Token { get; set; }
	public DateTime? Expiration { get; set; }
	public string? RefreshToken { get; set; }
	public DateTime? RefreshTokenExpiration { get; set; }

	public LoginDto()
	{
	}

	public LoginDto(string? token, DateTime? expiration, string? refreshToken, DateTime? refreshTokenExpiration) =>
		(Token, Expiration, RefreshToken, RefreshTokenExpiration) = (token, expiration, refreshToken, refreshTokenExpiration);
}