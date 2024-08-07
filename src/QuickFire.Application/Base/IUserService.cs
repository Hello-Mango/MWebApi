﻿using QuickFire.Application.DTOS.Request;
using QuickFire.Core.Dependency;
using QuickFire.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Application.Base
{
    public interface IUserService : IBaseInterface<SysUser>, IScopeDependency
    {
        public SysUser CreateUser(SysUser user);
        public Task<SysUser> CheckLoginSync(LoginReq loginReq);

    }
}
