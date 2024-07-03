using QuickFire.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Business.UserBiz
{
    public interface IUserFactory
    {
        public User CreateUser(long userId);
    }
}
