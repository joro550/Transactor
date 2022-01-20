using System;
using Polly;
using Xunit;
using System.Threading.Tasks;
using Transactor.Tests.Polly.Steps;
using Transactor.Tests.Polly.Results;

namespace Transactor.Tests.Polly;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var policy = Policy
            .Handle<Exception>()
            .Retry();

        var wrapper = new PolicyWrapper(policy);
        var workflow = new WorkflowBuilder<MyContext>()
            .AddStep(new IdIncrementStep(wrapper))
            .Build();

        var result = workflow.Execute();
        Assert.Equal(1, result.Context.Id);
    }
    
    [Fact]
    public async Task Test2()
    {
        var policy = Policy
            .Handle<Exception>()
            .RetryAsync();

        var wrapper = new AsyncPolicyWrapper(policy);
        var workflow = new AsyncWorkflowBuilder<MyContext>()
            .AddStep(new IdIncrementAsyncStep(wrapper))
            .Build();

        var result = await workflow.ExecuteAsync();
        Assert.Equal(1, result.Context.Id);
    }
}