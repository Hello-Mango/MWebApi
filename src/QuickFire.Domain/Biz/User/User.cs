using QuickFire.Domain.Entites;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Biz.User
{
    public class User : BaseAggregateRoot
    {
        public SysUser _user { get; private set; }
        private List<SysRole> _roles = new List<SysRole>();
        public IReadOnlyCollection<SysRole> Roles => _roles.AsReadOnly();

        public User(SysUser user, List<SysRole> roles)
        {
            _user = user;
            _roles = roles;
        }
        public SysUser CreateUser(SysUser user)
        {
            return user;
        }
        // 用户相关的方法，如添加角色、移除角色等
        public void AssignRole(SysRole role)
        {
            if (!_roles.Contains(role))
            {
                _roles.Add(role);
            }
        }

        public void RemoveRole(SysRole role)
        {
            if (_roles.Contains(role))
            {
                _roles.Remove(role);
            }
        }
    }
}
