using System.Threading.Tasks;
using Xunit;

namespace Transactor.Tests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var workflow = new WorkflowBuilder()
            .AddStep(new MyStep())
            .Build();

        var result = await workflow.ExecuteAsync();

    }
}

public class MyResult
{
    public int Id { get; set; }
}

public class MyStep : Step<MyResult>
{
    public override MyResult Execute()
    {
        // Insert into database
        return new MyResult() { Id = 1};
    }

    public override void RollBack(MyResult result)
    {
    }
}