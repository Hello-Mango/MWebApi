using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFireApi.Core
{
    public class UserContext
    {
        public long UserId { get; set; }
        public string UserName { get; set; }

        public long TenantId { get; set; }
    }
}
