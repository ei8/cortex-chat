using ei8.Cortex.Chat.Application.Identity;
using System;

namespace ei8.Cortex.Chat.Port.Adapter.IO.Process.Services
{
    public class TokenProviderService : ITokenProviderService
    {
        public string XsrfToken { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }

    public class InitialApplicationState
    {
        public string XsrfToken { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }
}
