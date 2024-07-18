using Hangfire;

namespace WebApplication1;

public class Test1Job
{
    [JobDisplayName("test 1")]
    [Queue("queue-a")]
    public Task Handle()
    {
        throw new NotImplementedException();
    }
}