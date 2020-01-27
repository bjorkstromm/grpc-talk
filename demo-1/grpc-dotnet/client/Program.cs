using System;
using System.Threading.Tasks;
using Greet.Net;
using Grpc.Net.Client;

namespace client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter name:");
            var name = Console.ReadLine();

            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client =  new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
