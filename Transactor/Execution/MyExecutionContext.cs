using Transactor.Steps;
using Transactor.Steps.Visitors;

namespace Transactor.Execution;

internal class MyExecutionContext<T> : IVisitor<T> where T : IExecutionContext, new ()
{
    private readonly T _state;

    public MyExecutionContext(T state) 
        => _state = state;

    public Task Visit(Step<T> step, CancellationToken cancellationToken = default)  
    {
        step.Execute(_state);
        return Task.CompletedTask;
    }

    public async Task Visit(AsyncStep<T> step, CancellationToken cancellationToken = default) 
        => await step.Execute(cancellationToken);
}