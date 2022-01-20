using System;
using System.Threading;
using System.Threading.Tasks;
using Transactor.Steps;
using Transactor.Tests.Results;

namespace Transactor.Tests.Steps;

public class ThrowExceptionStep : Step<MyContext>
{
    public override void Execute(MyContext executionContext) 
        => throw new NotImplementedException();

    public override void RollBack(MyContext executionContext)
        => throw new NotImplementedException();
}

public class AsyncThrowExceptionStep : AsyncStep<MyContext>
{
    public override Task ExecuteAsync(MyContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task RollBackAsync(MyContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}