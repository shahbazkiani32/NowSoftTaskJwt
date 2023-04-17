namespace NowSoft.Application.DTOs;

public class ChangePasswordDto
{
	public string? Email { get; set; }
	public string? CurrentPassword { get; set; }
	public string? NewPassword { get; set; }

	public ChangePasswordDto()
	{
	}

	public ChangePasswordDto(string? email, string? currentPassword, string? newPassword) =>
		(Email, CurrentPassword, NewPassword) = (email, currentPassword, newPassword);
}