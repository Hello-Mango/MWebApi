using QuickFire.BizException;
using QuickFire.Core;
using QuickFire.Core.Dependency;
using QuickFire.Domain.Entites;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Biz.User
{
    public class UserFactory : IUserFactory
    {
        private readonly IRepository<SysUser> _repositoryUser;
        public UserFactory(IRepository<SysUser> repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public SysUser CreateUser(long userId)
        {
            var sysUser = _repositoryUser.FindById(userId);
            if (sysUser is not null)
            {
                throw new EnumException(ExceptionEnum.USER_NOT_FOUND);
            }
            return sysUser!;
        }
    }
}
