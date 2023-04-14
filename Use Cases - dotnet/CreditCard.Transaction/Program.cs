using CreditCard.Transaction.Models;
using System;

namespace CreditCard.Transaction
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //Get Amount from user in Decimals
                Console.WriteLine("Please enter Amount: ");
                var amount = Convert.ToDecimal(Console.ReadLine());

                //Get City from user
                Console.WriteLine("Please enter City: ");
                var city = Console.ReadLine().ToLower();
                Console.WriteLine("\n");

                var transactionDetails = new TransactionDetails
                {
                    TransactionId = Guid.NewGuid().ToString(),
                    Amount = amount,
                    City = city
                };

                //Call Producer to validate and post
                Producer.PostEventToKafka(transactionDetails);
            }
        }
    }
}
