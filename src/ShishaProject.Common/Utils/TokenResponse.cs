using System;

namespace ShishaProject.Common.Utils
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }

        public DateTime? Expiration { get; set; }

        public string ReasonPhrase { get; set; }
    }
}
