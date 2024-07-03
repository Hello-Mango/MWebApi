using QuickFire.Application.Interface;
using QuickFire.Core.Dependency;
using QuickFire.Domain.Business.UserBiz;
using QuickFire.Domain.Entity;
using QuickFire.Domain.Shared;
using QuickFire.Infrastructure;

namespace QuickFire.Application.Services
{
    public class UserService : BaseService<SysDbContext, TUser>, IUserService
    {
        private readonly IUserFactory _userFactory;
        public UserService(IUnitOfWork<SysDbContext> unitOfWork, IUserFactory userFactory) : base(unitOfWork)
        {
            _userFactory = userFactory;
        }
        public TUser CreateUser(TUser user)
        {
            _unitOfWork.GetRepository<TUser>().Add(user);
            return user;
        }
    }
}
