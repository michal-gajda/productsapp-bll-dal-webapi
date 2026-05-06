namespace WebAPI;

using BLL;
using DAL;
using DAL.Data;
using Microsoft.OpenApi;
using WebAPI.Middleware;

public sealed class Program
{
    private Program()
    {
    }

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddBusinessLogic(builder.Configuration);
        builder.Services.AddDataAccess(builder.Configuration);
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Products API",
                Version = "v1",
                Description = "Przykładowe API do zarządzania produktami — architektura BLL/DAL/UI",
                Contact = new OpenApiContact { Name = "Developer", Email = "dev@example.com", },
            });
        });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        _ = Task.Run(async () =>
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await db.Database.EnsureCreatedAsync();
        });

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API v1");
            c.RoutePrefix = "swagger";
        });

        app.UseCors();
        app.MapControllers();

        await app.RunAsync();
    }
}
