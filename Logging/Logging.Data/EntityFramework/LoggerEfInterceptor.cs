using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace Logging.Data.EntityFramework
{
    public class LoggerEfInterceptor : IDbCommandInterceptor
    {
        private Exception WrapEntityFrameworkException(DbCommand command, Exception ex)
        {
            var newException = new Exception("EntityFramework command failed!", ex);
            AddParamsToException(command.Parameters, newException);
            return newException;
        }

        private void AddParamsToException(DbParameterCollection parameters, Exception exception)
        {
            foreach (DbParameter parameter in parameters)
            {
                exception.Data.Add(parameter.ParameterName, parameter.Value.ToString());
            }
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (interceptionContext.Exception != null)
                interceptionContext.Exception = WrapEntityFrameworkException(command, interceptionContext.Exception);
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (interceptionContext.Exception != null)
                interceptionContext.Exception = WrapEntityFrameworkException(command, interceptionContext.Exception);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (interceptionContext.Exception != null)
                interceptionContext.Exception = WrapEntityFrameworkException(command, interceptionContext.Exception);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }
    }
}
