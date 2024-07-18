
using Hangfire;
using WebApplication2;

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
    o.Queues = ["queue-b"];
});

var app = builder.Build();
app.UseHangfireDashboard();

var scope = app.Services.CreateScope().ServiceProvider;
var jobManager1 = scope.GetRequiredService<IBackgroundJobClient>();
var jobManager2 = scope.GetRequiredService<IRecurringJobManager>();

jobManager1.Enqueue<Test2Job>(x => x.Handle());
jobManager2.AddOrUpdate<Test2Job>("test-2", x => x.Handle(), "5 * * * *");

app.Run();