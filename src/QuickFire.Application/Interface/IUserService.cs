using QuickFire.Core.Dependency;
using QuickFire.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Application.Interface
{
    public interface IUserService : IBaseInterface<SysUser>, IScopeDependency
    {
        public SysUser CreateUser(SysUser user);
    }
}
