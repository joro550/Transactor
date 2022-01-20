using System.Threading.Tasks;
using Transactor.Tests.Results;
using Transactor.Tests.Steps;
using Xunit;

namespace Transactor.Tests;

public class AsyncTests
{
    [Fact]
    public async Task Test1()
    {
        var workflow = new AsyncWorkflowBuilder<MyContext>()
            .AddStep(new AsyncIdIncrementStep())
            .Build();

        var result = await workflow.ExecuteAsync();
        Assert.Equal(1, result.Context.Id);
    }
    
    [Fact]
    public async Task Test2()
    {
        var baseStep = new AsyncIdIncrementStep();
        
        var workflow = new AsyncWorkflowBuilder<MyContext>()
            .AddStep(baseStep)
            .AddStep(baseStep)
            .Build();

        var result = await workflow.ExecuteAsync();
        Assert.Equal(2, result.Context.Id);
    }
    
    [Fact]
    public async Task Test3()
    {
        var workflow = new AsyncWorkflowBuilder<MyContext>()
            .AddStep(new AsyncIdIncrementStep())
            .AddStep(new AsyncThrowExceptionStep())
            .Build();

        var result = await workflow.ExecuteAsync();
        Assert.Equal(0, result.Context.Id);
    }
    
    [Fact]
    public async Task Test4()
    {
        var workflow = new AsyncWorkflowBuilder<MyContext>()
            .AddStep(new AsyncIdIncrementStep())
            .AddStep(new AsyncThrowExceptionStep())
            .AddStep(new AsyncIdIncrementStep())
            .Build();

        var result = await workflow.ExecuteAsync();
        Assert.Equal(0, result.Context.Id);
    }
    
    [Fact]
    public async Task Test5()
    {
        var workflow =new AsyncWorkflowBuilder<MyContext>()
            .AddStep(new AsyncIdIncrementStep())
            .AddStep(new AsyncIdIncrementStep())
            .AddStep(new AsyncThrowExceptionStep())
            .Build();

        var result = await workflow.ExecuteAsync();
        Assert.Equal(0, result.Context.Id);
    }
}