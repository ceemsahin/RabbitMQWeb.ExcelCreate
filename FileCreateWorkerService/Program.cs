using FileCreateWorkerService;
using FileCreateWorkerService.Models;
using FileCreateWorkerService.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;


public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {


                IConfiguration Configuration = hostContext.Configuration;

                services.AddDbContext<AdventureWorks2019Context>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
                });
                services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
                services.AddHostedService<Worker>();
                services.AddSingleton<RabbitMQClientService>();
            });

}