using System;

namespace CreditCard.Consumers
{
    class Program
    {
        static void Main(string[] args)
        {
            //Call Consumers to Run
            Console.WriteLine("Starting Consumers...");
            Consumer.PassTransactionTopicConsumer();
            Consumer.HoldTransactionTopicConsumer();

            while (true)
            {
                Console.ReadKey();
            }
        }
    }
}
