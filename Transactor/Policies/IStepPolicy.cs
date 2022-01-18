namespace Transactor.Policies;

public interface IStepPolicy
{
    void Execute(Action action);
}

public class DefaultStepPolicy : IStepPolicy
{
    public static IStepPolicy Instance = new DefaultStepPolicy();
    
    public void Execute(Action action) 
        => action();
}

public interface IAsyncStepPolicy
{
    Task Execute(Func<CancellationToken, Task> action, CancellationToken token);
}

public class DefaultAsyncStepPolicy : IAsyncStepPolicy
{
    public static IAsyncStepPolicy Instance = new DefaultAsyncStepPolicy();

    public async Task Execute(Func<CancellationToken, Task> action, CancellationToken token)
        => await action(token);
}