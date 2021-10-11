using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CounterState;
using Microsoft.Extensions.Hosting;

namespace CounterStateServer
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly ICounterStateViewModel _counterStateViewModel;
        public KafkaConsumerService(ICounterStateViewModel counterStateViewModel)
        {
            _counterStateViewModel = counterStateViewModel;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "127.0.0.1:9092",
                GroupId = "tester1",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var topics = new[] { "test_topic" };
            
            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(topics);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = consumer.Consume(stoppingToken);

                    // handle consumed message.
                    _counterStateViewModel.Message = consumeResult.Message.Value;
                }
    
                consumer.Close();
            }
            
            return Task.CompletedTask;
        }
    }
}