namespace QuickFireApi.Extensions.JWT
{
    public class MJWTConfig
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Expires { get; set; }
        public int RefreshExpiration { get; set; }
    }
}
