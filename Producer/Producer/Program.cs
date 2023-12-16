
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;





static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((context, collection) =>
        {
            collection.AddHostedService<KafkaProducerHostedService>();
        });


CreateHostBuilder(args).Build().Run();

Console.WriteLine("Hello, World!");



public class KafkaProducerHostedService : IHostedService
{
    private IProducer<Null, string> _producer;

    public KafkaProducerHostedService()
    {
        var config = new ProducerConfig()
        {
            BootstrapServers = "localhost:9092",
            //SecurityProtocol = SecurityProtocol.Plaintext,
            //SaslMechanism = SaslMechanism.Gssapi,
            //SaslKerberosServiceName = kafka

        };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        for (int i = 0; i < 100; i++)
        {
            var value = $"Hello World {i}";
            await _producer.ProduceAsync("kafka_demo", new Message<Null, string>()
            {
                Value = value
            }, cancellationToken);
        }

        _producer.Flush(TimeSpan.FromSeconds(10));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _producer?.Dispose();
        return Task.CompletedTask;
    }
}