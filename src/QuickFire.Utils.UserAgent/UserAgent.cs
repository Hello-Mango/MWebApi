using UAParser;

namespace QuickFire.Utils.UserAgent
{
    public class UserAgent
    {
        public UserAgentInfo GetUserAgent(string uaString)
        {
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(uaString);

            return new UserAgentInfo();
        }
    }
}
