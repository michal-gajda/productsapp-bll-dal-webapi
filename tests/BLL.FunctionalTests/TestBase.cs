namespace BLL.FunctionalTests;

using BLL;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public abstract class TestBase
{
    protected ServiceProvider ServiceProvider { get; }

    protected TestBase()
    {
        var services = new ServiceCollection();

        var collection = new Dictionary<string, string?>();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(collection)
            .Build();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddLogging();

        services.AddBusinessLogic(configuration);
        services.AddDataAccess(configuration);

        this.ServiceProvider = services.BuildServiceProvider();
    }
}
