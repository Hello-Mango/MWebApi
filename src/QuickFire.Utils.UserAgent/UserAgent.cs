using UAParser;

namespace QuickFire.Utils.UserAgent
{
    public static class UserAgent
    {
        public static UserAgentInfo GeSysUserAgent(string uaString)
        {
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(uaString);

            return new UserAgentInfo()
            {
                Browser= c.UA.ToString(),
                OS = c.OS.ToString(),
                Device = c.Device.ToString(),
            };
        }
    }
}
