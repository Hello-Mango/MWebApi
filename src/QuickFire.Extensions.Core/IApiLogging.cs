using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.Core
{
    public interface IApiLogging
    {
        public Task AddLog(ApiLoggingModel context);
    }
}
