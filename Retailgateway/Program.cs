using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOcelot();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin() // Allow any origin
                      .AllowAnyMethod() // Allow any method (GET, POST, etc.)
                      .AllowAnyHeader(); // Allow any header
            });
        });
        builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors();
        app.UseHttpsRedirection();
        app.UseRouting();


        app.UseOcelot().Wait();

        app.MapControllers();

        app.Run();
    }
}
