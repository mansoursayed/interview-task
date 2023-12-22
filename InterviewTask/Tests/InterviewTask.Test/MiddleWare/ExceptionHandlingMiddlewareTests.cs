using InterviewTask.Application.Api.Middleware;
using InterviewTask.Application.Contract.IHelpers;
using InterviewTask.Application.IRepositories;
using InterviewTask.Application.IServices;
using InterviewTask.Infrastructure.Helper;
using InterviewTask.Infrastructure.Repositories;
using InterviewTask.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using Xunit;

namespace InterviewTask.Test.MiddleWare;
public class ExceptionHandlingMiddlewareTests
{

    [Fact]
    public async Task ExceptionHandlingMiddlewareTests_WhenNoErrors_ShouldReturnOk()
    {
        using var host = await new HostBuilder()
    .ConfigureWebHost(webBuilder =>
    {
        webBuilder
            .UseTestServer()
            .ConfigureServices(services =>
            {
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                services.AddScoped<IHelper, Helper>();


                services.AddScoped<IDriverService, DriverService>();

                services.AddScoped<IDriverRepository, DriverRepository>();

                services.AddTransient<ExceptionHandlingMiddleware>();
            })
            .Configure(app =>
            {
                app.UseMiddleware<ExceptionHandlingMiddleware>();
            });
    })
    .StartAsync();

        var response = await host.GetTestClient().GetAsync("/api/drivers/getAllDrivers");

        Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
    }


}