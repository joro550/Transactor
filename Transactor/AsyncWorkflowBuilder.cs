using Transactor.Execution;
using Transactor.Steps;

namespace Transactor;

public class AsyncWorkflowBuilder<T> where T : IExecutionContext, new ()  
{
    private readonly List<AsyncStep<T>> _steps 
        = new();

    private T _state = new T();

    public AsyncWorkflowBuilder<T> AddStep(AsyncStep<T> step)
    {
        _steps.Add(step);
        return this;
    }
    
    public AsyncWorkflowBuilder<T> WithInitialState(T state)
    {
        _state = state;
        return this;
    }

    public IAsyncOrchestrator<T> Build() 
        => new AsyncOrchestrator<T>(_steps, _state);
}