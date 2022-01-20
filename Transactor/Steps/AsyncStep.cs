using Transactor.Execution;
using Transactor.Policies;
using Transactor.Steps.Visitors;

namespace Transactor.Steps;

public abstract class AsyncStep<T> where T : IExecutionContext, new ()  
{
    private readonly IAsyncStepPolicy _stepPolicy;

    public AsyncStep(IAsyncStepPolicy stepPolicy) => _stepPolicy = stepPolicy;

    public AsyncStep() : this(DefaultAsyncStepPolicy.Instance) { }

    internal IAsyncStepPolicy GetPolicy() => _stepPolicy;
    
    public abstract Task ExecuteAsync(T context, CancellationToken cancellationToken = default);
    public abstract Task RollBackAsync(T context, CancellationToken cancellationToken = default);
    
    internal async Task Accept(IVisitor<T> visitor, CancellationToken cancellationToken = default) 
        => await visitor.Visit(this, cancellationToken);
}