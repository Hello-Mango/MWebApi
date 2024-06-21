using QuickFire.Core;

namespace QuickFireApi.Extensions
{
    public class UserPermission : IUserPermission
    {
        public bool CheckPermission(string userId, string permissionName)
        {
            return true;
        }
    }
}
