using Confluent.Kafka;
using Kafka.Public;
using Kafka.Public.Loggers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer
{
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
}
