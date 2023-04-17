namespace NowSoft.Application.DTOs;

public class RegistrationDto
{
	public string? UserName { get; set; }
	public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Device { get; set; }
    public string? IpAddress { get; set; }
}