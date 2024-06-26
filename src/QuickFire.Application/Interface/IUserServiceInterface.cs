using QuickFire.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Application.Interface
{
    public interface IUserServiceInterface : IBaseInterface<TUser>
    {
        public TUser CreateUser(TUser user);
    }
}
