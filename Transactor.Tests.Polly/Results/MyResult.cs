using Transactor.Execution;

namespace Transactor.Tests.Polly.Results;

public class MyContext : IExecutionContext
{
    public int Id { get; set; }
}