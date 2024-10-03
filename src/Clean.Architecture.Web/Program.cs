using System.Reflection;
using Ardalis.ListStartupServices;
using Ardalis.SharedKernel;
using Clean.Architecture.Core.ContributorAggregate;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Infrastructure;
using Clean.Architecture.Infrastructure.Data;
using Clean.Architecture.Infrastructure.Email;
using Clean.Architecture.UseCases.Contributors.Create;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using Serilog;
using Serilog.Extensions.Logging;
using Clean.Architecture.Web.Middlewares;
using Clean.Architecture.UseCases.Abstractions;
using Clean.Architecture.Infrastructure.Respositories;
using Clean.Architecture.Web.Fishbowl;
using Microsoft.Extensions.DependencyInjection;
using Clean.Architecture.Infrastructure.BigCommerce;
using Clean.Architecture.UseCases.Abstractions.Respository;
using System.Collections;
using Clean.Architecture.Web.Schedule;
using Quartz;


var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
var microsoftLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

builder.Services.AddScoped<IFishbowlRespository, FishbowlRespository>();
builder.Services.AddScoped<IBigCommerceRepository, BigCommerceRepository>();
builder.Services.AddHttpClient<FishbowlContext>().ConfigurePrimaryHttpMessageHandler(() =>
{
  return new SocketsHttpHandler() { PooledConnectionLifetime = TimeSpan.FromSeconds(15) };
}).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
builder.Services.AddHttpClient<B2C_V2_Context>().ConfigurePrimaryHttpMessageHandler(() =>
{
  return new SocketsHttpHandler() { PooledConnectionLifetime = TimeSpan.FromSeconds(15) };
}).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
builder.Services.AddHttpClient<B2C_V3_Context>().ConfigurePrimaryHttpMessageHandler(() =>
{
  return new SocketsHttpHandler() { PooledConnectionLifetime = TimeSpan.FromSeconds(15) };
}).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
builder.Services.AddHttpClient<B2B_Context>().ConfigurePrimaryHttpMessageHandler(() =>
{
  return new SocketsHttpHandler() { PooledConnectionLifetime = TimeSpan.FromSeconds(15) };
}).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
// Configure Web Behavior
builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddFastEndpoints()
                .SwaggerDocument(o =>
                {
                  o.ShortSchemaNames = true;
                });

ConfigureMediatR();

var hashtable = Environment.GetEnvironmentVariables();
foreach(DictionaryEntry entry in hashtable)
{
  Console.WriteLine(entry.Key + ":" + entry.Value); 
}

builder.Services.AddInfrastructureServices(builder.Configuration, microsoftLogger);
builder.Services.AddQuartz();
builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);


if (builder.Environment.IsDevelopment())
{
  // Use a local test email server
  // See: https://ardalis.com/configuring-a-local-test-email-server/
  builder.Services.AddScoped<IEmailSender, MimeKitEmailSender>();

  // Otherwise use this:
  //builder.Services.AddScoped<IEmailSender, FakeEmailSender>();
  AddShowAllServicesSupport();
}
else
{
  builder.Services.AddScoped<IEmailSender, MimeKitEmailSender>();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
}
else
{
  app.UseDefaultExceptionHandler(); // from FastEndpoints
  app.UseHsts();
}

var schedulerFactory = app.Services.GetService<ISchedulerFactory>();

IJobDetail job = JobBuilder.Create<MyJob>()
  .WithIdentity("myJob", "group1")
  .Build();

ITrigger trigger = TriggerBuilder.Create()
  .WithIdentity("myTrigger", "group1")
  .StartNow()
  .WithCronSchedule("0 */1 * ? * *", x=>x.WithMisfireHandlingInstructionFireAndProceed())
  .ForJob("myJob", "group1")
  .Build();

if (schedulerFactory != null)
{
  var scheduler = await schedulerFactory.GetScheduler();
  await scheduler.ScheduleJob(job, trigger);

}

var now = DateTime.Now;
TimeZoneInfo pacificTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
DateTime utcNow = TimeZoneInfo.ConvertTime(now, pacificTimeZone);
Console.WriteLine("***START TIME***");
Console.WriteLine(utcNow.ToString() + " - Pacific Time" );
Console.WriteLine(utcNow.ToLongTimeString());
Console.WriteLine("***END TIME***");



app.UseAuthenticationMiddleware();
app.UseFastEndpoints()
    .UseSwaggerGen(); // Includes AddFileServer and static files middleware

app.UseHttpsRedirection();

await SeedDatabase(app);

app.Run();


static async Task SeedDatabase(WebApplication app)
{
  using var scope = app.Services.CreateScope();
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<AppDbContext>();
    //          context.Database.Migrate();
    context.Database.EnsureCreated();
    await SeedData.InitializeAsync(context);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}

void ConfigureMediatR()
{
  var mediatRAssemblies = new[]
{
  Assembly.GetAssembly(typeof(Contributor)), // Core
  Assembly.GetAssembly(typeof(CreateContributorCommand)), // UseCases
};
  builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
  builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
  builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
}

void AddShowAllServicesSupport()
{
  // add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
  builder.Services.Configure<ServiceConfig>(config =>
  {
    config.Services = new List<ServiceDescriptor>(builder.Services);

    // optional - default path to view services is /listallservices - recommended to choose your own path
    config.Path = "/listservices";
  });
}

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program
{
}
