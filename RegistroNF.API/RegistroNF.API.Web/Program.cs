using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Infrastructure;
using RegistroNF.API.IOC;
using RegistroNF.API.ScheduledJobs;
using RegistroNF.API.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.InjectRepositories(builder.Configuration)
                .InjectServices()
                .InjectValidators()
                .InjectSMTPServices();

builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<SMTPSettings>(builder.Configuration.GetSection("SmtpSettings"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHangfireDashboard();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

RecurringJob.AddOrUpdate<APIJob>("get-cadastro-parcial",
    job => job.ExecuteAsync(), Cron.Monthly(1, 7));

app.Run();