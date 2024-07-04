using QuickFire.Application.Interface;
using QuickFire.Core.Dependency;
using QuickFire.Domain.Biz.User;
using QuickFire.Domain.Entity;
using QuickFire.Domain.Shared;
using QuickFire.Infrastructure;

namespace QuickFire.Application.Services
{
    public class UserService : BaseService<SysDbContext, SysUser>, IUserService
    {
        private readonly IUserFactory _userFactory;
        public UserService(IUnitOfWork<SysDbContext> unitOfWork, IUserFactory userFactory) : base(unitOfWork)
        {
            _userFactory = userFactory;
        }
        public SysUser CreateUser(SysUser user)
        {
            _unitOfWork.GetRepository<SysUser>().Add(user);
            return user;
        }

        public async bool CheckLoginInfo(string userName, string password)
        {
            var user = await _unitOfWork.GetRepository<SysUser>().FindAsync(x => x.Mobile == userName || x.Email == userName);
            return true;
        }
    }
}
