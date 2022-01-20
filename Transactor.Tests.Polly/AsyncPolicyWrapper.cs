using System;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Transactor.Policies;

namespace Transactor.Tests.Polly;

public class AsyncPolicyWrapper : IAsyncStepPolicy
{
    private readonly AsyncPolicy _policy;

    public AsyncPolicyWrapper(AsyncPolicy policy) 
        => _policy = policy;

    public async Task ExecuteAsync(Func<CancellationToken, Task> action, CancellationToken token) 
        => await _policy.ExecuteAsync(action, token);
}