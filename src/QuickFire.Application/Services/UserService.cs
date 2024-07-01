using QuickFire.Application.Interface;
using QuickFire.Core.Dependency;
using QuickFire.Domain.Entity;
using QuickFire.Domain.Shared;
using QuickFire.Infrastructure;

namespace QuickFire.Application.Services
{
    public class UserService : BaseService<ApplicationDbContext, TUser>, IUserService
    {
        public UserService(IUnitOfWork<ApplicationDbContext> unitOfWork) : base(unitOfWork)
        {

        }
        public TUser CreateUser(TUser user)
        {
            _unitOfWork.GetRepository<TUser>().Add(user);
            return user;
        }
    }
}
