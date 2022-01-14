using Transactor.Execution;

namespace Transactor.Steps.Visitors;

public class ExecutionResult<T> where T: IExecutionContext, new()
{
    public bool Success { get; }
    public T Context { get;  }

    private ExecutionResult(bool success, T context)
    {
        Success = success;
        Context = context;
    }

    internal static ExecutionResult<TResult> Thing<TResult>(TResult item) where TResult : IExecutionContext, new() =>
        new(true, item);
    
    internal static ExecutionResult<TResult> Fail<TResult>(TResult item) where TResult : IExecutionContext, new() =>
        new(false, item);
}