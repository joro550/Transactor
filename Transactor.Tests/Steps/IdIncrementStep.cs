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