using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFireApi.Response
{
    public class TokenResponse
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public long Timestamp { get; set; }
    }
}
