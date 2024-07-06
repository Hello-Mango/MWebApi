using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.Extensions.Core
{
    public interface IBaseSessionContext<Tkey>
    {
        public Tkey UserId { get; }
        public string UserName { get; }
        public string IpAddress { get; }
        public List<string> Roles { get; }
        public Tkey TenantId { get; }
        public void SetsessionContext(Tkey userId, string userName, List<string> roles);
        public void SetTenant(Tkey TenantId);
        public void SetIp(string ipAddress);
    }

    public interface ISessionContext: IBaseSessionContext<long>
    {
      
    }
}
