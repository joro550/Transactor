using Transactor.Execution;
using Transactor.Steps;

namespace Transactor;

public class WorkflowBuilder<T> where T : IExecutionContext, new ()  
{
    private readonly List<Step<T>> _steps 
        = new();

    public WorkflowBuilder<T> AddStep(Step<T> step)
    {
        _steps.Add(step);
        return this;
    }

    public IOrchestrator<T> Build() 
        => new Orchestrator<T>(_steps);
}