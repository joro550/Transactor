using System.Threading;
using System.Threading.Tasks;
using Transactor.Steps;
using Transactor.Tests.Results;

namespace Transactor.Tests.Steps;

public class IdIncrementStep : Step<MyContext>
{
    public override void Execute(MyContext executionContext) 
        => executionContext.Id++;

    public override void RollBack(MyContext executionContext)
        => executionContext.Id--;
}

public class AsyncIdIncrementStep : AsyncStep<MyContext>
{
    public override Task ExecuteAsync(MyContext context, CancellationToken cancellationToken = default)
    {
        context.Id++;
        return Task.CompletedTask;
    }

    public override Task RollBackAsync(MyContext context, CancellationToken cancellationToken = default)
    {
        context.Id--;
        return Task.CompletedTask;
    }
}