using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Internship_backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        // Configure the application services and request pipeline in Startup.cs
        app.ConfigureServices();
        app.Configure();

        
        
        
        app.Run();
    }
}
