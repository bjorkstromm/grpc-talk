using System;
using System.Threading.Tasks;
using Greet.Net;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;

namespace client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client =  channel.CreateGrpcService<IGreeter>();
            var reply = await client.SayHello(new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
