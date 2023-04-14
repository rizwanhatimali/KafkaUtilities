using Confluent.Kafka;
using CreditCard.Transaction.Enum;
using CreditCard.Transaction.Models;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CreditCard.Consumers
{
    public static class Consumer
    {
        public static void PassTransactionTopicConsumer()
        {
            var cts = new CancellationTokenSource();
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = Constant.BootstrapServers,
                GroupId = Constant.ConsumerGroup,
                AutoOffsetReset = Constant.OffsetReset
            };

            var consumeTask = Task.Run(() =>
            {
                using var consumer = new ConsumerBuilder<string, string>(consumerConfig)
                        .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                        .Build();
                consumer.Subscribe(EventTopic.PassTransactionTopic.ToString());

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(cts.Token);
                            var result = JsonConvert.DeserializeObject<TransactionDetails>(consumeResult.Message.Value);
                            Console.WriteLine($"Consumer 1: Your Tranaction for Rs.{result.Amount} from {result.City} is Approved.");
                            Console.WriteLine();
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Consume1 error: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            });
        }

        public static void HoldTransactionTopicConsumer()
        {
            var cts = new CancellationTokenSource();
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = Constant.BootstrapServers,
                GroupId = Constant.ConsumerGroup,
                AutoOffsetReset = Constant.OffsetReset
            };
            var consumeTask = Task.Run(() =>
            {
                using var consumer = new ConsumerBuilder<string, string>(consumerConfig)
                        .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                        .Build();
                consumer.Subscribe(EventTopic.HoldTransactionTopic.ToString());

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(cts.Token);
                            var result = JsonConvert.DeserializeObject<TransactionDetails>(consumeResult.Message.Value);
                            Console.WriteLine($"Consumer 2: A Transaction of Rs.{result.Amount} from {result.City} is kept on hold, user confirmation is needed.");
                            Console.WriteLine();
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Consume2 error: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            });
        }
    }
}
