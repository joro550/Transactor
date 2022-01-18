using Transactor.Execution;
using Transactor.Policies;
using Transactor.Steps.Visitors;

namespace Transactor.Steps;

public abstract class AsyncStep<T> : BaseStep<T> where T : IExecutionContext, new ()  
{
    private readonly IAsyncStepPolicy _stepPolicy;

    public AsyncStep(IAsyncStepPolicy stepPolicy) => _stepPolicy = stepPolicy;

    public AsyncStep() : this(DefaultAsyncStepPolicy.Instance) { }

    internal IAsyncStepPolicy GetPolicy() => _stepPolicy;
    
    public abstract Task Execute(CancellationToken cancellationToken = default);
    public abstract Task RollBack(CancellationToken cancellationToken = default);
    
    internal override async Task Accept(IVisitor<T> visitor, CancellationToken cancellationToken = default) 
        => await visitor.Visit(this, cancellationToken);
}