using System.Security.Claims;

namespace PRODUCTSERVICE.API.Helpers
{
  public class UserHelpers
  {
    public static string GetUserEmail(ClaimsPrincipal user)
    {
      return user.FindFirst(_ => _.Type.Equals(ClaimTypes.Email)).Value;
    }
    public static string GetUserPhone(ClaimsPrincipal user)
    {
      return user.FindFirst(_ => _.Type.Equals(ClaimTypes.MobilePhone)).Value;
    }
  }
}