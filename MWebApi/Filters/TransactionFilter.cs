using Microsoft.AspNetCore.Mvc.Filters;
using SqlSugar;

namespace MWebApi.Filters
{
    public class TransactionFilter : IActionFilter
    {
        //你也可以换EF CORE对象 或者ADO对象都行
        ISqlSugarClient _db;
        //（ISqlSugarClient）需要IOC注入处理事务的对象
        public TransactionFilter(ISqlSugarClient db)
        {
            _db = db;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _db.Ado.BeginTran();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                _db.Ado.CommitTran();
            }
            else
            {
                _db.Ado.RollbackTran();
            }
        }
    }
}
