using System;
using System.Collections.Generic;
using System.Text;

namespace MWebApi.Core
{
    public interface IUserPermission
    {
        bool CheckPermission(string userId, string permissionName);
    }
}
