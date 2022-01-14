using Transactor.Execution;
using Transactor.Steps.Visitors;

namespace Transactor.Steps;

public abstract class AsyncStep<T> : BaseStep<T> where T : IExecutionContext, new ()  
{
    public abstract Task Execute(CancellationToken cancellationToken = default);
    public abstract Task RollBack(CancellationToken cancellationToken = default);
    
    internal override async Task Accept(IVisitor<T> visitor, CancellationToken cancellationToken = default) 
        => await visitor.Visit(this, cancellationToken);
}