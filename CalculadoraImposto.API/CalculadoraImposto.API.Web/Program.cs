using CalculadoraImposto.API.ScheduledJobs;
using CalculadoraImposto.API.IOC;
using Hangfire;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectRepositories()
                .InjectServices()
                .InjectValidators();

builder.Services.AddHttpClient<APIJob>();
builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

RecurringJob.AddOrUpdate<APIJob>("get-notas-mensal", 
    job => job.ExecuteAsync(), Cron.Monthly(1, 7));

app.Run();