using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.Extensions.Core
{
    public interface IUserPermission
    {
        bool CheckPermission(string userId, string permissionName);
    }
}
