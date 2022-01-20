using System.Threading;
using System.Threading.Tasks;
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

public class IdIncrementAsyncStep : AsyncStep<MyContext>
{
    public IdIncrementAsyncStep(IAsyncStepPolicy policy) : base(policy) { }
    
    public override Task ExecuteAsync(MyContext executionContext, CancellationToken token)
    {
        executionContext.Id++;
        return Task.CompletedTask;
    } 

    public override Task RollBackAsync(MyContext executionContext, CancellationToken token)
    {
        executionContext.Id--;
        return Task.CompletedTask;
    }
}