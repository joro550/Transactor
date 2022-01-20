using Xunit;
using Transactor.Tests.Steps;
using Transactor.Tests.Results;

namespace Transactor.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var workflow = new WorkflowBuilder<MyContext>()
            .AddStep(new IdIncrementStep())
            .Build();

        var result = workflow.Execute();
        Assert.Equal(1, result.Context.Id);
    }
    
    [Fact]
    public void Test2()
    {
        var baseStep = new IdIncrementStep();
        
        var workflow = new WorkflowBuilder<MyContext>()
            .AddStep(baseStep)
            .AddStep(baseStep)
            .Build();

        var result = workflow.Execute();
        Assert.Equal(2, result.Context.Id);
    }
    
    [Fact]
    public void Test3()
    {
        var workflow = new WorkflowBuilder<MyContext>()
            .AddStep(new IdIncrementStep())
            .AddStep(new ThrowExceptionStep())
            .Build();

        var result = workflow.Execute();
        Assert.Equal(0, result.Context.Id);
    }
    
    [Fact]
    public void Test4()
    {
        var workflow = new WorkflowBuilder<MyContext>()
            .AddStep(new IdIncrementStep())
            .AddStep(new ThrowExceptionStep())
            .AddStep(new IdIncrementStep())
            .Build();

        var result = workflow.Execute();
        Assert.Equal(0, result.Context.Id);
    }
    
    [Fact]
    public void Test5()
    {
        var workflow = new WorkflowBuilder<MyContext>()
            .AddStep(new IdIncrementStep())
            .AddStep(new IdIncrementStep())
            .AddStep(new ThrowExceptionStep())
            .Build();

        var result = workflow.Execute();
        Assert.Equal(0, result.Context.Id);
    }
}