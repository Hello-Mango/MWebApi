using Microsoft.AspNetCore.Mvc.Filters;
using SqlSugar;

namespace MWebApi.Filters
{
    public class TransactionFilterAttribute : IActionFilter
    {
        ISqlSugarClient _db;
        public TransactionFilterAttribute(ISqlSugarClient db)
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
