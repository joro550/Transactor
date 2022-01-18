using System;
using Polly;
using Xunit;
using Transactor.Policies;
using System.Threading.Tasks;
using Transactor.Tests.Polly.Steps;
using Transactor.Tests.Polly.Results;

namespace Transactor.Tests.Polly;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var policy = Policy
            .Handle<Exception>()
            .Retry();

        var wrapper = new PolicyWrapper(policy);
        var workflow = new WorkflowBuilder<MyContext>()
            .AddStep(new IdIncrementStep(wrapper))
            .Build();

        var result = await workflow.ExecuteAsync();
        Assert.Equal(1, result.Context.Id);
    }
}

public class PolicyWrapper : IStepPolicy
{
    private readonly Policy _policy;

    public PolicyWrapper(Policy policy) 
        => _policy = policy;

    public void Execute(Action action) 
        => _policy.Execute(action);
} 