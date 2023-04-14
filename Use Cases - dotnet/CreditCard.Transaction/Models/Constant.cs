using Confluent.Kafka;
using System.Collections.Generic;

namespace CreditCard.Transaction.Models
{
    public static class Constant
    {
        static Constant()
        {
            //make it localhost:9092 if kafka is running on your local machine
            BootstrapServers = "10.54.57.13:9092";
            ConsumerGroup = "Consumer-Group-1";
            OffsetReset = AutoOffsetReset.Latest;
            City = new List<string>
            {
                "new delhi",
                "delhi",
                "noida",
                "gurgaon"
            };
            TransactionUpperLimit = 50000.00m;  //50 thousand
        }
        public static string BootstrapServers { get; }
        public static string ConsumerGroup { get; }
        public static AutoOffsetReset OffsetReset { get; }
        public static IList<string> City { get; }
        public static decimal TransactionUpperLimit { get; }
    }
}