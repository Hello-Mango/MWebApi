using MWebApi.Core;

namespace MWebApi.Extensions
{
    public class UserPermission : IUserPermission
    {
        public bool CheckPermission(string userId, string permissionName)
        {
            return true;
        }
    }
}
