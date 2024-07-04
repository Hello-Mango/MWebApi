using QuickFire.Core;
using QuickFire.Core.Dependency;
using QuickFire.Domain.Entity;
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

        public User CreateUser(long userId)
        {
            var SysUser = _repositoryUser.FindById(userId);
            if (SysUser is not null)
            {
                throw new Exception422("User not found");
            }
            User user = new User(SysUser!, new List<SysRole>());
            return user;
        }
    }
}
