using InterviewTask.Application.Api.Middleware;
using InterviewTask.Application.Contract.IHelpers;
using InterviewTask.Application.IRepositories;
using InterviewTask.Application.IServices;
using InterviewTask.Infrastructure.Helper;
using InterviewTask.Infrastructure.Repositories;
using InterviewTask.Infrastructure.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IHelper, Helper>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
var app = builder.Build();

// ensure database and tables exist
{
    using var scope = app.Services.CreateScope();
    var repository = scope.ServiceProvider.GetRequiredService<IDriverRepository>();
    await repository.InitDrivertableAsync();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
