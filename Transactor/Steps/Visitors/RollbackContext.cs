using Transactor.Execution;

namespace Transactor.Steps.Visitors;

internal class RollbackContext<T> : IVisitor<T> where T : IExecutionContext, new ()
{
    private readonly T _state;

    public RollbackContext(T state) 
        => _state = state;
    
    public Task Visit(Step<T> step, CancellationToken cancellationToken = default)
    {
        step.RollBack(_state);
        return Task.CompletedTask;
    }

    public async Task Visit(AsyncStep<T> step, CancellationToken cancellationToken = default) 
        => await step.RollBack(cancellationToken);
}