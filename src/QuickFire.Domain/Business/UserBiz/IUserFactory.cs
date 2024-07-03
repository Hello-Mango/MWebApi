using QuickFire.Core.Dependency;
using QuickFire.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Business.UserBiz
{
    public interface IUserFactory: IScopeDependency
    {
        public User CreateUser(long userId);
    }
}
