using System;
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