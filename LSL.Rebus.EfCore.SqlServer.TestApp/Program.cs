using LSL.Rebus.EfCore.SqlServer.TestApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hb, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddDbContext<AppDbContext>(o => 
        {
            o.UseSqlServer(hb.Configuration.GetConnectionString("MyDb")!);
        });
    })
    .Build();

host.Run();
