﻿namespace QuickFireApi.Models.Reponse
{
    public class LoginReponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Timestamp { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public long CompanyId { get; set; }
    }
}
