using Transactor.Execution;

namespace Transactor.Steps.Visitors;

internal class RollbackContext<T> : IVisitor<T> where T : IExecutionContext, new ()
{
    private readonly T _state;

    public RollbackContext(T state) 
        => _state = state;

    public void Visit(Step<T> step)
        => step.GetPolicy().Execute(() => step.RollBack(_state));

    public async Task Visit(AsyncStep<T> step, CancellationToken cancellationToken = default) 
        => await step.RollBackAsync(_state, cancellationToken);
}