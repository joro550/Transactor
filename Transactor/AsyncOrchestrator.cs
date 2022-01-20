using Transactor.Execution;
using Transactor.Steps;
using Transactor.Steps.Visitors;

namespace Transactor;

internal class AsyncOrchestrator<T> : IAsyncOrchestrator<T> where T : IExecutionContext, new ()
{
    private readonly T _state;
    private readonly List<AsyncStep<T>> _steps;

    public AsyncOrchestrator(List<AsyncStep<T>> steps, T initialState)
    {
        _steps = steps;
        _state = initialState;
    }

    public async Task<ExecutionResult<T>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        ExecutionResult<T>? result = null;
        
        var myExecutionContext = new ExecutionVisitor<T>(_state);
        for (var stepsCompleted = 0; stepsCompleted < _steps.Count; stepsCompleted++)
        {
            try
            {
                await _steps[stepsCompleted].Accept(myExecutionContext, cancellationToken);
            }
            catch (Exception)
            {
                await Rollback(stepsCompleted, _state, cancellationToken);
                result = ExecutionResult<T>.Fail(_state);
                break;
            }
        }

        return result ?? ExecutionResult<T>.Successful(_state);
    }

    private async Task Rollback(int stepsCompleted, T state, CancellationToken cancellationToken = default)
    {
        var rollbackContext = new RollbackContext<T>(state);
        for (var i = 0; i <= stepsCompleted; i++)
            try
            {
                await _steps[i].Accept(rollbackContext, cancellationToken);
            }
            catch (Exception)
            {
                continue;
            }
    }
}