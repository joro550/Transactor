using Transactor.Execution;

namespace Transactor.Steps.Visitors;

internal interface IVisitor<T> where T : IExecutionContext, new ()
{
    Task Visit(Step<T> step, CancellationToken cancellationToken = default);
    Task Visit(AsyncStep<T> step, CancellationToken cancellationToken = default);
}