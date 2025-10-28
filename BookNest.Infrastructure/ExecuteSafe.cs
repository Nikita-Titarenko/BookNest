using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace BookNest.Infrastructure
{
    public class ExecuteSafe : IExecuteSafe
    {
        private readonly ILogger<ExecuteSafe> _logger;

        public ExecuteSafe(ILogger<ExecuteSafe> logger) {
            _logger = logger;
        }
        public async Task<Result<T>> ExecuteSafeAsync<T>(Func<Task<Result<T>>> func)
        {
            try
            {
                return await func();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail(new Error(ex.Message).WithMetadata("Code", ex.Number));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail(new Error(ex.Message).WithMetadata("Code", 0));
            }
        }

        public async Task<Result> ExecuteSafeAsync(Func<Task<Result>> func)
        {
            try
            {
                return await func();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail(new Error(ex.Message).WithMetadata("Code", ex.Number));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail(new Error(ex.Message).WithMetadata("Code", 0));
            }
        }
    }
}
