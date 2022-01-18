using Transactor.Execution;

namespace Transactor.Steps.Visitors;

internal class ExecutionVisitor<T> : IVisitor<T> where T : IExecutionContext, new ()
{
    private readonly T _state;

    public ExecutionVisitor(T state) 
        => _state = state;

    public Task Visit(Step<T> step, CancellationToken cancellationToken = default)
    {
        step.GetPolicy()
            .Execute(() => step.Execute(_state));
        
        return Task.CompletedTask;
    }

    public async Task Visit(AsyncStep<T> step, CancellationToken cancellationToken = default)
        => await step.GetPolicy()
            .Execute(async ct => await step.Execute(ct), cancellationToken);
}