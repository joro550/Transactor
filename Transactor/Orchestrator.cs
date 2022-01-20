﻿using Transactor.Execution;
using Transactor.Steps;
using Transactor.Steps.Visitors;

namespace Transactor;

internal class Orchestrator<T> : IOrchestrator<T> where T : IExecutionContext, new ()
{
    private readonly List<Step<T>> _steps;

    public Orchestrator(List<Step<T>> steps) 
        => _steps = steps;

    public ExecutionResult<T> Execute()
    {
        var context = new T();
        ExecutionResult<T>? result = null;
        
        var myExecutionContext = new ExecutionVisitor<T>(context);
        for (var stepsCompleted = 0; stepsCompleted < _steps.Count; stepsCompleted++)
        {
            try
            {
                _steps[stepsCompleted].Accept(myExecutionContext);
            }
            catch (Exception)
            {
                Rollback(stepsCompleted, context);
                result = ExecutionResult<T>.Fail(context);
                break;
            }
        }

        return result ?? ExecutionResult<T>.Thing(context);
    }

    private void Rollback(int stepsCompleted, T state)
    {
        var rollbackContext = new RollbackContext<T>(state);
        for (var i = 0; i <= stepsCompleted; i++)
            try
            {
                _steps[i].Accept(rollbackContext);
            }
            catch (Exception)
            {
                continue;
            }
    }
}