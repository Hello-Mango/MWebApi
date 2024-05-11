using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFireApi.Core
{
    public class TokenResponse
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public long Timestamp { get; set; }
    }
}
