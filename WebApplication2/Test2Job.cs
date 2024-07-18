using Hangfire;

namespace WebApplication2;

public class Test2Job
{
    [JobDisplayName("test 2")]
    [Queue("queue-b")]
    public Task Handle()
    {
        throw new NotImplementedException();
    }
}