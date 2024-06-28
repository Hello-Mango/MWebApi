using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.Extensions.Core
{
    public interface IUserContext
    {
        public long UserId { get; }
        public string UserName { get; }
        public long TenantId { get; }
        public List<string> Roles { get; }

        public void SetUserContext(long userId, string userName, long tenantId,List<string> roles);
    }
}
