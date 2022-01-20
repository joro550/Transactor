using Transactor.Execution;

namespace Transactor.Steps.Visitors;

internal interface IVisitor<T> where T : IExecutionContext, new ()
{
    void Visit(Step<T> step);
    Task Visit(AsyncStep<T> step, CancellationToken cancellationToken = default);
}