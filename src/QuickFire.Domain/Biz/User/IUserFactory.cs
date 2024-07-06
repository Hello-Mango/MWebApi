using QuickFire.Core.Dependency;
using QuickFire.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Biz.User
{
    public interface IUserFactory: IScopeDependency
    {
        public SysUser CreateUser(long userId);
    }
}
