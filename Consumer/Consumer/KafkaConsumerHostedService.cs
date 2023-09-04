using System.Text;
using Kafka.Public;
using Kafka.Public.Loggers;
using Microsoft.Extensions.Hosting;

namespace Consumer;

public class KafkaConsumerHostedService : IHostedService
{
    private ClusterClient _cluster;

    public KafkaConsumerHostedService()
    {
        _cluster = new ClusterClient(new Configuration()
        {
            Seeds = "localhost:9092",

        }, new ConsoleLogger());
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cluster.ConsumeFromLatest("kafka_demo");
        _cluster.MessageReceived += record =>
        {
            Console.WriteLine(Encoding.UTF8.GetString(record.Value as byte[]));
        };

        return Task.CompletedTask;

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cluster?.Dispose();
        return Task.CompletedTask;
    }
}