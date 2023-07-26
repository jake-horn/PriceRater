using System.IdentityModel.Tokens.Jwt;

namespace PriceRater.API.Authentication.Helpers
{
    public interface IJwtService
    {
        public string GenerateJwtToken(int id);

        public JwtSecurityToken VerifyJwtToken(string jwtToken);
    }
}
