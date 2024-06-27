using QuickFire.Extensions.Core;

namespace QuickFire.Extensions.UserContext
{
    public class UserContext : IUserContext
    {
        public long UserId { get; private set; }
        public string UserName { get; private set; }
        public long TenantId { get; private set; }

        public void SetUserContext(long userId, string userName, long tenantId)
        {
            UserId = userId;
            UserName = userName;
            TenantId = tenantId;
        }
    }
}
