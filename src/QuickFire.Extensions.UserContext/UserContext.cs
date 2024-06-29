using QuickFire.Extensions.Core;

namespace QuickFire.Extensions.UserContext
{
    public class UserContext : IUserContext
    {
        public long UserId { get; private set; }
        public string UserName { get; private set; }
        public long TenantId { get; private set; }

        public List<string> Roles => new List<string>();

        public string IpAddress { get; private set; }

        public void SetUserContext(long userId, string userName, long tenantId, List<string> roles, string ipAddress)
        {
            UserId = userId;
            UserName = userName;
            TenantId = tenantId;
            IpAddress = ipAddress;
            Roles.AddRange(roles);
        }
    }
}
