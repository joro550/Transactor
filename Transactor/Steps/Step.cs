using Transactor.Execution;
using Transactor.Policies;
using Transactor.Steps.Visitors;

namespace Transactor.Steps;

public abstract class Step<T> : BaseStep<T> where T : IExecutionContext, new ()  
{
    private readonly IStepPolicy _policy;

    public Step() : this(DefaultStepPolicy.Instance)
    {
    }

    public Step(IStepPolicy policy) 
        => _policy = policy;

    internal IStepPolicy GetPolicy() => _policy;

    public abstract void Execute(T executionResult);
    public abstract void RollBack(T executionResult);

    internal override async Task Accept(IVisitor<T> visitor, CancellationToken cancellationToken = default) 
        => await visitor.Visit(this, cancellationToken);
}