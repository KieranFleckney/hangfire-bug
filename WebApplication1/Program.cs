
using Hangfire;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(x =>
{
    x
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage("server=KIERANFLECKNEY;initial catalog=attb_hangfire;Trusted_Connection=True;TrustServerCertificate=True;");
});

builder.Services.AddHangfireServer(o =>
{
    o.Queues = ["queue-a"];
});

var app = builder.Build();
app.UseHangfireDashboard();

var scope = app.Services.CreateScope().ServiceProvider;
var jobManager1 = scope.GetRequiredService<IBackgroundJobClient>();
var jobManager2 = scope.GetRequiredService<IRecurringJobManager>();

jobManager1.Enqueue<Test1Job>(x => x.Handle());
jobManager2.AddOrUpdate<Test1Job>("test-1", x => x.Handle(), "5 * * * *");

app.Run();