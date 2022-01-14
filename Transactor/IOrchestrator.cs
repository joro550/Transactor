﻿using Transactor.Execution;
using Transactor.Steps.Visitors;

namespace Transactor;

public interface IOrchestrator<T> where T : IExecutionContext, new ()
{
    Task<ExecutionResult<T>> ExecuteAsync(CancellationToken cancellationToken = default);
}