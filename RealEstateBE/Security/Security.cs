using System.IdentityModel.Tokens.Jwt;

namespace RealEstateBE.Security
{
    public interface ISecurity
    {
        public bool IsAuthenticatedByToken(string token, Guid userID);
    }
    public class Security: ISecurity
    {
        public bool IsAuthenticatedByToken(string token, Guid userID)
        {
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.ReadJwtToken(token);
                return securityToken.Claims.SingleOrDefault(c => c.Type.Equals("ID"))!.Value.Equals(userID.ToString());
        }
    }
}
