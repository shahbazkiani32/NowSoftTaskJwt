namespace NowSoft.Application.DTOs;

public class AuthenticationDto
{
	public string? UserName { get; set; }
	public string? Password { get; set; }

	/// <summary>
	/// 
	/// </summary>
	public AuthenticationDto()
	{
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="email"></param>
	/// <param name="password"></param>
	public AuthenticationDto(string? email, string? password) => (UserName, Password) = (email, password);
}