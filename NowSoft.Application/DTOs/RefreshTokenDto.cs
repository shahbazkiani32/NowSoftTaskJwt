namespace NowSoft.Application.DTOs;

public class RefreshTokenDto
{
	public string? Token { get; set; }
	public string? RefreshToken { get; set; }

	public RefreshTokenDto()
	{
	}

	public RefreshTokenDto(string? token, string? refreshToken) => (Token, RefreshToken) = (token, refreshToken);
}