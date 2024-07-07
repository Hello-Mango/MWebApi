using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using QuickFire.Core;
using QuickFire.Domain.Shared;

namespace QuickFire.Domain.Entites
{
    public class SysUser : BaseEntity
    {
        [Comment("用户编号")]
        [StringLength(50)]
        public string No { get; set; }
        [Comment("用户名称")]
        [StringLength(50)]
        public string Name { get; set; }

        [Comment("用户邮箱")]
        [StringLength(50)]
        public string Email { get; set; }

        [Comment("用户密码")]
        [StringLength(120)]
        public string Password { get; set; }

        [Comment("用户手机号")]
        [StringLength(20)]
        public string Mobile { get; set; }

        [Comment("用户是否锁定标记 0：正常 1：锁定")]
        public bool IsLock { get; set; }

        private List<long> RoleIds = new List<long>();

        public bool CheckPassword(string password)
        {
            if (password == Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetRoles(List<long> roleIds)
        {
            RoleIds.Clear();
            RoleIds.AddRange(roleIds);
        }
        public List<long> GetRoles()
        {
            return RoleIds;
        }
        public override string ToString()
        {
            return string.Format("{0}({1})", Name, No);
        }
        public void SetDisabled()
        {
            IsLock = true;
        }
        public void SetEnabled()
        {
            IsLock = false;
        }
        public void SetPassword(string password)
        {
            Password = password;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }
        public void SetMobile(string mobile)
        {
            Mobile = mobile;
        }
    }
}
