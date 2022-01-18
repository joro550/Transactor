using Transactor.Policies;
using Transactor.Steps;
using Transactor.Tests.Polly.Results;

namespace Transactor.Tests.Polly.Steps;

public class IdIncrementStep : Step<MyContext>
{
    public IdIncrementStep(IStepPolicy policy) : base(policy) { }
    
    public override void Execute(MyContext executionContext) 
        => executionContext.Id++;

    public override void RollBack(MyContext executionContext)
        => executionContext.Id--;
}