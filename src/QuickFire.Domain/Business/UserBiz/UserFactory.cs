using QuickFire.Core;
using QuickFire.Core.Dependency;
using QuickFire.Domain.Entity;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Business.UserBiz
{
    public class UserFactory : IUserFactory, IScopeDependency
    {
        private readonly IRepository<TUser> _repositoryUser;
        public UserFactory(IRepository<TUser> repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public User CreateUser(long userId)
        {
            var tUser = _repositoryUser.FindById(userId);
            if (tUser is not null)
            {
                throw new Exception422("User not found");
            }
            User user = new User(tUser!, new List<TRole>());
            return user;
        }
    }
}
