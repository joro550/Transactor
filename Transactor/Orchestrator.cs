using Transactor.Steps;
using Transactor.Execution;
using Transactor.Steps.Visitors;

namespace Transactor;

internal class Orchestrator<T> : IOrchestrator<T> where T : IExecutionContext, new ()
{
    private readonly T _state;
    private readonly List<Step<T>> _steps;

    public Orchestrator(List<Step<T>> steps, T initialState)
    {
        _steps = steps;
        _state = initialState;
    }

    public ExecutionResult<T> Execute()
    {
        ExecutionResult<T>? result = null;
        
        var myExecutionContext = new ExecutionVisitor<T>(_state);
        for (var stepsCompleted = 0; stepsCompleted < _steps.Count; stepsCompleted++)
        {
            try
            {
                _steps[stepsCompleted].Accept(myExecutionContext);
            }
            catch (Exception)
            {
                Rollback(stepsCompleted, _state);
                result = ExecutionResult<T>.Fail(_state);
                break;
            }
        }

        return result ?? ExecutionResult<T>.Successful(_state);
    }

    private void Rollback(int stepsCompleted, T state)
    {
        var rollbackContext = new RollbackContext<T>(state);
        for (var i = 0; i <= stepsCompleted; i++)
            try
            {
                _steps[i].Accept(rollbackContext);
            }
            catch (Exception)
            {
                continue;
            }
    }
}