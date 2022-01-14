using Transactor.Execution;

namespace Transactor.Tests.Results;

public class MyContext : IExecutionContext
{
    public int Id { get; set; }
}