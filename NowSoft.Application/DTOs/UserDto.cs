namespace NowSoft.Application.DTOs;

public class UserDto
{
	public string? UserName { get; set; }
	public string? Email { get; set; }

	public UserDto()
	{
	}

	public UserDto(string? userName, string? email) => (UserName, Email) = (userName, email);
}