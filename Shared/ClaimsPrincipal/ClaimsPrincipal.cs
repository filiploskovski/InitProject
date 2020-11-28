using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Shared.ClaimsPrincipal
{
    public class ClaimsPrincipalProvider : IClaimsPrincipal
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsPrincipalProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                try
                {
                    return Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims
                        .FirstOrDefault(claim => claim.Type.ToLower() == "userid")
                        ?.Value);
                }
                catch (Exception e)
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }
    }
}