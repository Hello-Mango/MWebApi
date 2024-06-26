using QuickFire.Domain.Entity;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Services
{
    public class UserAggregateRoot : BaseAggregateRoot<long>
    {
        public TUser _user { get; private set; }
        private List<TRole> _roles = new List<TRole>();
        public IReadOnlyCollection<TRole> Roles => _roles.AsReadOnly();

        public UserAggregateRoot(TUser user, List<TRole> roles)
        {
            Id = user.Id;
            _user = user;
            _roles = roles;
        }
        public TUser CreateUser(TUser user)
        {
            return user;
        }
        // 用户相关的方法，如添加角色、移除角色等
        public void AssignRole(TRole role)
        {
            if (!_roles.Contains(role))
            {
                _roles.Add(role);
            }
        }

        public void RemoveRole(TRole role)
        {
            if (_roles.Contains(role))
            {
                _roles.Remove(role);
            }
        }
    }
}
