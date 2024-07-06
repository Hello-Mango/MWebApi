using QuickFire.Application.Base;
using QuickFire.Application.DTOS.Request;
using QuickFire.BizException;
using QuickFire.Core;
using QuickFire.Core.Dependency;
using QuickFire.Domain.Biz.User;
using QuickFire.Domain.Entites;
using QuickFire.Domain.Shared;
using QuickFire.Infrastructure;
using QuickFire.Utils;

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

        public async Task<SysUser> CheckLoginSync(LoginReq loginReq)
        {
            var user = await _unitOfWork.GetRepository<SysUser>().FindAsync(x => x.Mobile == loginReq.userAccount || x.Email == loginReq.userAccount);
            if (user == null)
            {
                throw new BizException.EnumException(ExceptionEnum.USER_NOT_FOUND, loginReq.userAccount);
            }
            string password = EncryptUtils.EncryptStringToMd5(loginReq.password);
            bool flag = user.CheckPassword(password);
            return user;
        }
    }
}
