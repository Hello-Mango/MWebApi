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
    public class UserService : BaseService< SysUser>, IUserService
    {
        private readonly IUserFactory _userFactory;
        public UserService(IUnitOfWork<ApplicationDbContext> unitOfWork, IUserFactory userFactory) : base(unitOfWork)
        {
            _userFactory = userFactory;
        }
        public SysUser CreateUser(SysUser user)
        {
            _unitOfWork.GetLongRepository<SysUser>().Add(user);
            return user;
        }

        public async Task<SysUser> CheckLoginSync(LoginReq loginReq)
        {
            var user = await _unitOfWork.GetLongRepository<SysUser>().FindAsync(x => x.Mobile == loginReq.userAccount || x.Email == loginReq.userAccount);
            if (user == null)
            {
                throw new BizException.EnumException(ExceptionEnum.USER_NOT_FOUND, loginReq.userAccount);
            }
            string password = EncryptUtils.EncryptStringToMd5(loginReq.password);
            bool flag = user.CheckPassword(password);
            if (flag)
            {
                var roles = await _unitOfWork.GetLongRepository<TSysUserRole>().FindByAsync(Z => Z.UserId == user.Id);
                user.SetRoles(roles.Select(x => x.RoleId).ToList());
                return user;
            }
            else
            {
                throw new BizException.EnumException(ExceptionEnum.USER_PASSWORD_ERROR, loginReq.userAccount);
            }
        }
    }
}
