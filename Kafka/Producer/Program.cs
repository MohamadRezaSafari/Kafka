using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Producer;
using System.Text;




//var kafka = new KafkaProducerController();
//kafka.SendToKafka("kafka test", "hello");

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
            collection.AddHostedService<KafkaConsumerHostedService>();
            collection.AddHostedService<KafkaProducerHostedService>();
        });
}


