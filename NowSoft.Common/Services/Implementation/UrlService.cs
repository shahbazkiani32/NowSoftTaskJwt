
namespace NowSoft.Common.Services.Implementation;

public class UrlService : IUrlService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public UrlService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public string GetCurrentRequestUrl()
	{
		string remoteIpAddress = string.Empty;
		if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
			remoteIpAddress = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString();
		return
			$"{_httpContextAccessor.HttpContext.Request.Scheme}://{remoteIpAddress}{_httpContextAccessor.HttpContext.Request.Path.Value.Replace("/api", "/gateway")}";
	}
}