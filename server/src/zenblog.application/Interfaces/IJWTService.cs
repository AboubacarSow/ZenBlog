using zenblog.application.Utilities;

namespace zenblog.application.Interfaces;

public interface IJWTService    
{
    Task<TokenContainer> CreateTokenAsync(bool populateExpireTime,ApplicationUser user);
}