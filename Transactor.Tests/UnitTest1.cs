using Xunit;
using System.Threading.Tasks;
using Transactor.Tests.Steps;
using Transactor.Tests.Results;

namespace Transactor.Tests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var workflow = new WorkflowBuilder<MyContext>()
            .AddStep(new IdIncrementStep())
            .Build();

        var result = await workflow.ExecuteAsync();
        Assert.Equal(1, result.Context.Id);
    }
    
    [Fact]
    public async Task Test2()
    {
        var baseStep = new IdIncrementStep();
        
        var workflow = new WorkflowBuilder<MyContext>()
            .AddStep(baseStep)
            .AddStep(baseStep)
            .Build();

        var result = await workflow.ExecuteAsync();
        Assert.Equal(2, result.Context.Id);
    }
    
    [Fact]
    public async Task Test3()
    {
        var workflow = new WorkflowBuilder<MyContext>()
            .AddStep(new IdIncrementStep())
            .AddStep(new ThrowExceptionStep())
            .Build();

        var result = await workflow.ExecuteAsync();
        Assert.Equal(0, result.Context.Id);
    }
    
    [Fact]
    public async Task Test4()
    {
        var workflow = new WorkflowBuilder<MyContext>()
            .AddStep(new IdIncrementStep())
            .AddStep(new ThrowExceptionStep())
            .AddStep(new IdIncrementStep())
            .Build();

        var result = await workflow.ExecuteAsync();
        Assert.Equal(0, result.Context.Id);
    }
    
    [Fact]
    public async Task Test5()
    {
        var workflow = new WorkflowBuilder<MyContext>()
            .AddStep(new IdIncrementStep())
            .AddStep(new IdIncrementStep())
            .AddStep(new ThrowExceptionStep())
            .Build();

        var result = await workflow.ExecuteAsync();
        Assert.Equal(0, result.Context.Id);
    }
}