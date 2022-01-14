# Transactor
 
When performing complex operations on a distributed system we usually want a way to perform the operation and rollback when things don't go the way we expect them to go.

This library help with that process, let's say we want to 
- Save a file 
- Update the database
- Send a message on a queue

First we define our seperate steps with their appropriate rollbacks

```csharp
public class MyContext : IExecutionContext
{
    public int DatabaseId {get;set;}
}


public class SaveFileStep : AsyncStep<MyContext>
{
    private readonly string _fileName;

    public SaveFileStep(string fileName)
    {
        _fileName = fileName;
    }

    public override Task Execute(CancellationToken cancellationToken = default)
    {
        // save file to directory
        return Task.CompletedTask;
    }

    public override Task RollBack(CancellationToken cancellationToken = default)
    {
        // Delete file?
        return Task.CompletedTask;
    }
}

public class UpdateDatabase : AsyncStep<MyContext>
{
    private readonly DatabaseContext _context;

    public UpdateDatabase(DatabaseContext context)
    {
        _context = context;
    }
    
    public override Task Execute(CancellationToken cancellationToken = default)
    {
        // save item to database
        return Task.CompletedTask;
    }

    public override Task RollBack(CancellationToken cancellationToken = default)
    {
        // Delete record?
        return Task.CompletedTask;
    }
}

public class SendMessage : AsyncStep<MyContext>
{
    private readonly Queue _context;

    public SendMessage(Queue context)
    {
        _context = context;
    }
    
    public override Task Execute(CancellationToken cancellationToken = default)
    {
        // Send message to queue
        return Task.CompletedTask;
    }

    public override Task RollBack(CancellationToken cancellationToken = default)
    {
        // ??
        return Task.CompletedTask;
    }
}
```

Then we list the steps in an order which we would want to operate, for example here we add the message to the queue last as this operation has no rollback

```csharp
var fileName = "C:/users/file.txt";
var context = new DatabaseContext();
var queue = new Queue();

var workflow = new WorkflowBuilder<MyContext>()
    .AddStep(new SaveFileOperation(fileName))
    .AddStep(new UpdateDatabase(context))
    .AddStep(new SendMessage(queue))
    .Build();
```

This now completes each step in order, if one fails then it will replay the tasks that fail.

```csharp
var result = await workflow.ExecuteAsync();
```

The result of this method is a type that tells you whether the workflow was a success and the context (in this case it would be `MyContext`)
```csharp
public class ExecutionResult<T> where T: IExecutionContext, new()
{
    public bool Success { get; }
    public T Context { get;  }

    private ExecutionResult(bool success, T context)
    {
        Success = success;
        Context = context;
    }
}
```

