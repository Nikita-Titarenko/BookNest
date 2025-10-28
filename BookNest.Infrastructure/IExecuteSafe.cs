using FluentResults;

namespace BookNest.Infrastructure
{
    public interface IExecuteSafe
    {
        Task<Result<T>> ExecuteSafeAsync<T>(Func<Task<Result<T>>> func);
        Task<Result> ExecuteSafeAsync(Func<Task<Result>> func);
    }
}