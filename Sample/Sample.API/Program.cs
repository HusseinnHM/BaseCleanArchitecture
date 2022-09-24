using BaseCleanArchitecture.API.Middlewares.ExceptionHandlerMiddleware;
using BaseCleanArchitecture.API.Middlewares.SeedMiddleware;
using BaseCleanArchitecture.DependencyInjection;
using BaseCleanArchitecture.DependencyInjection.OpenApi;
using Sample.API.DataSeed;
using Sample.Application.Core.Abstractions.Data;
using Sample.Infrastructure;
using Sample.Persistence;
using Sample.Persistence.Context;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddPersistence()
    .AddBaseCleanArchitecture<ISampleDbContext,SampleDbContext>(
        Sample.Application.AssemblyReference.Assembly,
        AssemblyReference.Assembly);


var app = builder.Build();

app.UseOpenApi(false);

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.SeedAsync<SampleDbContext>(DataSeed.Seed);
app.Run();

