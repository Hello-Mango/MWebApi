using QuickFire.Extensions.Core;

namespace QuickFire.Extensions.sessionContext
{
    public class SessionContext : ISessionContext
    {
        public long UserId { get; private set; }
        public string UserName { get; private set; }
        public long TenantId { get; private set; }
        public List<string> Roles => new List<string>();
        public string IpAddress { get; private set; }

        public void SetIp(string ipAddress)
        {
            IpAddress = ipAddress;
        }

        public void SetTenant(long tenantId)
        {
            TenantId = tenantId;
        }

        public void SetsessionContext(long userId, string userName, List<string> roles)
        {
            UserId = userId;
            UserName = userName;
            Roles.AddRange(roles);
        }
    }
}
