namespace zenblog.application.Common.Utilities;

public interface IJWTService
{
    Task<TokenDto> CreateTokenAsync(bool populateExpireTime,ApplicationUser user);
}

public record TokenDto(string AccessToken, string RefreshToken);