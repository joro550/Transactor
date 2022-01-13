namespace Transactor;

public abstract class BaseStep
{
    internal abstract Task<object> Accept(IVisitor visitor, CancellationToken cancellationToken = default);
}

internal interface IVisitor
{
    Task<TOut> HandleExecutionAsync<TOut>(Step<TOut> step) where TOut : class;
    Task HandleRollbackAsync<TOut>(Step<TOut> step, TOut param1) where TOut : class;
    Task<TOut> HandleExecutionAsync<TOut>(AsyncStep<TOut> step, CancellationToken cancellationToken = default) where TOut : class;
    Task HandleRollbackAsync<TOut>(AsyncStep<TOut> step, TOut param1, CancellationToken cancellationToken = default) where TOut : class;
}

internal class ExecutionContext : IVisitor
{
    public Task<TOut> HandleExecutionAsync<TOut>(Step<TOut> step)  where TOut : class
        => Task.FromResult(step.Execute());

    public Task HandleRollbackAsync<TOut>(Step<TOut> step, TOut param1) where TOut : class
        => Task.Run(() => step.RollBack(param1));

    public async Task<TOut> HandleExecutionAsync<TOut>(AsyncStep<TOut> step, CancellationToken cancellationToken = default) where TOut : class
        => await step.Execute();

    public async Task HandleRollbackAsync<TOut>(AsyncStep<TOut> step, TOut param1, CancellationToken cancellationToken = default) where TOut : class
        => await step.RollBack(param1);
}

public abstract class Step<TOut> : BaseStep where TOut :class
{
    public abstract TOut Execute();
    public abstract void RollBack(TOut result);

    internal override async Task<object> Accept(IVisitor visitor, CancellationToken cancellationToken = default) 
        => await visitor.HandleExecutionAsync(this);
}

public abstract class AsyncStep<TOut> :  BaseStep where TOut :class
{
    public abstract Task<TOut> Execute();
    public abstract Task RollBack(TOut param1);
    
    internal override async Task<object> Accept(IVisitor visitor, CancellationToken cancellationToken = default) 
        => await visitor.HandleExecutionAsync(this, cancellationToken);
}

public class WorkflowBuilder
{
    private readonly List<BaseStep> _steps 
        = new();

    public WorkflowBuilder AddStep(BaseStep step)
    {
        _steps.Add(step);
        return this;
    }

    public IOrchestrator Build() 
        => new Orchestrator(_steps);
}

public interface IOrchestrator
{
    Task<object> ExecuteAsync(CancellationToken cancellationToken = default);
}

internal class Orchestrator : IOrchestrator
{
    private readonly List<BaseStep> _steps;

    public Orchestrator(List<BaseStep> steps) 
        => _steps = steps;

    public async Task<object> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var stepsCompleted = 0;
        Exception exceptionCaught;

        for (; stepsCompleted < _steps.Count; stepsCompleted++)
        {
            try
            {            
                var result = _steps[stepsCompleted].Accept(new ExecutionContext(), cancellationToken);


            }
            catch (Exception e)
            {
                exceptionCaught = e;
            }
        }
        
        return new { };
    }

    private async Task Rollback()
    {
        
    }
}