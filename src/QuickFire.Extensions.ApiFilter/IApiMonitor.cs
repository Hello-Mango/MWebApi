namespace QuickFire.Extensions.ApiFilter
{
    public interface IApiMonitor
    {
        public Task Monitor(ApiMonitorModel context);
    }
}
