using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlobsFunctionApp;

public class BlogsKafkaConnectorFunction
{
    readonly private IKafkaService _kafkaService;
    readonly private ILogger<BlogsKafkaConnectorFunction> _logger;
    readonly private IFileStreamParser _fileStreamParser;

    public BlogsKafkaConnectorFunction(IKafkaService kafkaService, IFileStreamParser fileStreamParser, ILogger<BlogsKafkaConnectorFunction> logger) 
    {
        _kafkaService = kafkaService;
        _logger = logger;
        _fileStreamParser = fileStreamParser;
    }

    [FunctionName("BlogsKafkaConnectorFunction")]
    public async Task RunAsync([BlobTrigger("samples-workitems/{name}", Connection = "")]Stream myBlob, 
        string name, ILogger log)
    {
        _logger.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        
        var result = await _kafkaService.PublishAsync(
                topic: "simple-test", JsonConvert.SerializeObject(_fileStreamParser.Parse(name, myBlob)
            ));
    }
}
