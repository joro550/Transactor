using System;
using Transactor.Steps;
using Transactor.Tests.Polly.Results;

namespace Transactor.Tests.Polly.Steps;

public class ThrowExceptionStep : Step<MyContext>
{
    public override void Execute(MyContext executionContext) 
        => throw new NotImplementedException();

    public override void RollBack(MyContext executionContext)
        => throw new NotImplementedException();
}