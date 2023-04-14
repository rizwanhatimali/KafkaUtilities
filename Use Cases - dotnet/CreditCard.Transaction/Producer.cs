using Confluent.Kafka;
using CreditCard.Transaction.Enum;
using CreditCard.Transaction.Models;
using Newtonsoft.Json;
using System;

namespace CreditCard.Transaction
{
    public static class Producer
    {
        public static async void PostEventToKafka(TransactionDetails transactionDetails)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = Constant.BootstrapServers
            };

            var message = new Message<string, string>
            {
                Key = transactionDetails.TransactionId,
                Value = JsonConvert.SerializeObject(transactionDetails)
            };

            using var producer = new ProducerBuilder<string, string>(producerConfig).Build();

            //Validation of transaction
            if (!Constant.City.Contains(transactionDetails.City) && transactionDetails.Amount > Constant.TransactionUpperLimit)
            {
                //Suspicious Transaction, hold till user approval
                Console.WriteLine($"Transaction Id {transactionDetails.TransactionId}, Posted to topic {EventTopic.HoldTransactionTopic}");

                await producer.ProduceAsync(EventTopic.HoldTransactionTopic.ToString(), message)
                .ContinueWith(task => task.IsFaulted
                    ? $"error producing message: {task.Exception.Message}"
                    : $"produced to: {task.Result.TopicPartitionOffset}");

            }
            else
            {
                //Normal Transaction, Proceed further
                Console.WriteLine($"Transaction Id {transactionDetails.TransactionId}, Posted to topic {EventTopic.PassTransactionTopic}");
                
                await producer.ProduceAsync(EventTopic.PassTransactionTopic.ToString(), message)
                    .ContinueWith(task => task.IsFaulted
                        ? $"error producing message: {task.Exception.Message}"
                        : $"produced to: {task.Result.TopicPartitionOffset}");

            }
        }

    }
}
