using Transactor.Execution;
using Transactor.Steps.Visitors;

namespace Transactor.Steps;

public abstract class BaseStep<T>  where T : IExecutionContext, new ()
{
    internal abstract Task Accept(IVisitor<T> visitor, CancellationToken cancellationToken = default);
}