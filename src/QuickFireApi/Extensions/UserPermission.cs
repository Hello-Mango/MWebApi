using QuickFire.Core;
using QuickFire.Extensions.Core;

namespace QuickFireApi.Extensions
{
    public class UserPermission : IUserPermission
    {

        /// <summary>
        /// 校验用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public bool CheckPermission(string userId, string permissionName)
        {
            //todo 校验时候需要考虑租户信息
            return true;
        }
    }
}
