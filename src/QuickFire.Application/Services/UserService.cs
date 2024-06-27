using QuickFire.Application.Interface;
using QuickFire.Domain.Entity;
using QuickFire.Domain.Shared;

namespace QuickFire.Application.Services
{
    public class UserService : BaseService<TUser>, IUserService
    {
        public UserService(IUnitOfWork<TUser> unitOfWork) : base(unitOfWork)
        {

        }
        public TUser CreateUser(TUser user)
        {
            _unitOfWork.GetRepository<TUser>().Add(user);
            return user;
        }
    }
}
