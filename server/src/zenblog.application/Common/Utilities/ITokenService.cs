using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace zenblog.application.Common.Utilities;

public interface ITokenService
{
    Task<TokenDto> CreateTokenAsync(bool populateExpireTime);
}

internal class TokenService(IConfiguration configuration, ApplicationUser user, UserManager<ApplicationUser> userManager) : ITokenService
{
    public async Task<TokenDto> CreateTokenAsync(bool populateExpireTime)
    {
        //Generate Token Options
        var signInCredentials = GetSignInCredentials();
        var claims =  GetClaims();
        var tokenOptions = GenerateTokenOptions(signInCredentials,claims);
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        if(populateExpireTime)
            user.RefreshTokenExpiresTime = DateTime.Now.AddDays(7);
        await userManager.UpdateAsync(user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new TokenDto(
            AccessToken:accessToken,
            RefreshToken:refreshToken
        ) ;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = configuration.GetRequiredSection("JwtSettings");
        return  new JwtSecurityToken(
            issuer:jwtSettings["validIssuer"],
            audience:jwtSettings["validAudiance"],
            claims:claims,
            expires:DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["Expires"])),
            signingCredentials:signingCredentials
        );
    }

    private static string GenerateRefreshToken()
    {
        var randNumber = new byte[32];
        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(randNumber);
        return Convert.ToBase64String(randNumber);
    }

    private List<Claim> GetClaims()
    {
       var claims = new List<Claim>()
       {
           new(ClaimTypes.Name, user.UserName!),
           new (ClaimTypes.Email, user.Email!),
       };

       return claims;

    }

    private SigningCredentials GetSignInCredentials()
    {
        var jwtSettings= configuration.GetRequiredSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]!);
        var secretKey = new SymmetricSecurityKey(key);
        return new SigningCredentials(key:secretKey,algorithm:SecurityAlgorithms.HmacSha256);
    }
}

public record TokenDto(string AccessToken, string RefreshToken);