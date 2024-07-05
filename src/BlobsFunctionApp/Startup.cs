using BlobsFunctionApp;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace BlobsFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IKafkaService, KafkaService>();
            builder.Services.AddTransient<IFileStreamParser, FileStreamParser>();
        }
    }
}
