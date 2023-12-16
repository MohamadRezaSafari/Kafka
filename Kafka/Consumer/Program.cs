
using Consumer;
using Kafka.Public;
using Kafka.Public.Loggers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;


//services.AddSingleton<IHostedService, KafkaConsumerHandler>();

class Program
{
    static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }


    private static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((context, collection) =>
        {
            collection.AddHostedService<KafkaConsumerHandler>();
        });



}


//public class KafkaConsumerHostedService : IHostedService
//{
//    private ClusterClient _cluster;

//    public KafkaConsumerHostedService()
//    {
//        _cluster = new ClusterClient(new Configuration()
//        {
//            Seeds = "localhost:9092",

//        }, new ConsoleLogger());
//    }

//    public Task StartAsync(CancellationToken cancellationToken)
//    {
//        _cluster.ConsumeFromLatest("kafka_demo");
//        _cluster.MessageReceived += record =>
//        {
//            Console.WriteLine(Encoding.UTF8.GetString(record.Value as byte[]));
//        };

//        return Task.CompletedTask;

//    }

//    public Task StopAsync(CancellationToken cancellationToken)
//    {
//        _cluster?.Dispose();
//        return Task.CompletedTask;
//    }
//}
