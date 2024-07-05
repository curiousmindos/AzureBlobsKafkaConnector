using System.Threading.Tasks;
using Confluent.Kafka;

namespace BlobsFunctionApp;

public interface IKafkaService
{
    Task<string> PublishAsync(string topic, string data);
}

public class KafkaService : IKafkaService
{
    private ProducerConfig producerConfig;
    public KafkaService()
    {
        producerConfig = new ProducerConfig
        {
            // confluent.cloud
            BootstrapServers = "your service.confluent.cloud:9092",
            SecurityProtocol = SecurityProtocol.SaslSsl,
            SaslMechanism = SaslMechanism.Plain,
            SaslUsername = "your service setting",
            SaslPassword = "your service setting",
        };
    }

    public async Task<string> PublishAsync(string topic, string data)
    {
        using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();

        var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = data });
        
        return result.Value;
    }
}
