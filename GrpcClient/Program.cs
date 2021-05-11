using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = new HelloRequest { Name = "Guram" };
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(input);

            Console.WriteLine(reply.Message);
            Console.WriteLine("\n");

            var clientInput = new CustomerLookUpModel { UserId = 3 };
            var customerClient = new Customer.CustomerClient(channel);

            var clientReply = await customerClient.GetCustomerInfoAsync(clientInput);

            Console.WriteLine($"{clientReply.FirstName}  {clientReply.LastName} {clientReply.Age}\n\n");

            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;

                    Console.WriteLine($"{currentCustomer.FirstName}  {currentCustomer.LastName} {currentCustomer.Age}");
                }
            }

            Console.ReadLine();
        }
    }
}
