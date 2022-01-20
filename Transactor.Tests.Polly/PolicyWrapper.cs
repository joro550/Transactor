using System;
using Polly;
using Transactor.Policies;

namespace Transactor.Tests.Polly;

public class PolicyWrapper : IStepPolicy
{
    private readonly Policy _policy;

    public PolicyWrapper(Policy policy) 
        => _policy = policy;

    public void Execute(Action action) 
        => _policy.Execute(action);
}