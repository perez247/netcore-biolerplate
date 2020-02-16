using System;
using System.Security.Claims;

namespace Application.Infrastructure.Token
{
    /// <summary>
    /// This class is used to get the necessary token from the request and pass to the child class
    /// 
    /// Claess that wish to get this data has to inherit this class and
    /// </summary>
    public class TokenCredentials
    {
        public TokenCredentials()
        {}

        public string UserId { get; set; }
        public void setTokens(ClaimsPrincipal User) {
            SetUserId(User);
        }

        private void SetUserId(ClaimsPrincipal User)
        {
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
    
}