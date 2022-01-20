using Transactor.Execution;

namespace Transactor.Steps.Visitors;

internal class ExecutionVisitor<T> : IVisitor<T> where T : IExecutionContext, new ()
{
    private readonly T _state;

    public ExecutionVisitor(T state) 
        => _state = state;

    public void Visit(Step<T> step) 
        => step.GetPolicy().Execute(() => step.Execute(_state));

    public async Task Visit(AsyncStep<T> step, CancellationToken cancellationToken = default)
        => await step.GetPolicy()
            .ExecuteAsync(async ct => await step.ExecuteAsync(_state, ct), cancellationToken);
}