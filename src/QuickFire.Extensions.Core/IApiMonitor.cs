using System.Threading.Tasks;

namespace QuickFire.Extensions.Core
{
    public interface IApiMonitor
    {
        public Task Monitor(ApiMonitorModel context);
    }
}
