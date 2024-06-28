using QuickFire.Extensions.Core;

namespace QuickFire.Extensions.UserContext
{
    public class UserContext : IUserContext
    {
        public long UserId { get; private set; }
        public string UserName { get; private set; }
        public long TenantId { get; private set; }

        public List<string> Roles => new List<string>();

        public void SetUserContext(long userId, string userName, long tenantId, List<string> roles)
        {
            UserId = userId;
            UserName = userName;
            TenantId = tenantId;
            Roles.AddRange(roles);
        }
    }
}
