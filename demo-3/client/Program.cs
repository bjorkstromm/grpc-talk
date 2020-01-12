using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Time.Net;
using static Time.Net.TimeService;

namespace client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client =  new TimeServiceClient(channel);

            // Server streaming
            using var reply = client.Subscribe(new SubscribeRequest
            {
                Interval = 1,
                Count = 10,
                Unit = SubscribeRequest.Types.Unit.Seconds
            });

            await foreach (var timestamp in reply.ResponseStream.ReadAllAsync())
            {
                var dateTime = timestamp.ToDateTime();
                Console.WriteLine($"Time is {dateTime:o}");
            }

            // Client streaming
            var sync = client.Sync();

            for (var i = 0; i < 10; i++)
            {
                await sync.RequestStream.WriteAsync(
                    Timestamp.FromDateTime(DateTime.UtcNow));
                await Task.Delay(1000);
            }
            await sync.RequestStream.CompleteAsync();
            var response = await sync.ResponseAsync;

            Console.WriteLine(response.Message);

            // Bi-directional streaming
            var duplex = client.Ping(deadline: DateTime.UtcNow.AddSeconds(10));

            await duplex.RequestStream.WriteAsync(
                Timestamp.FromDateTime(DateTime.UtcNow));

            await foreach (var diff in duplex.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"Diff {diff.Value}");
                await duplex.RequestStream.WriteAsync(
                    Timestamp.FromDateTime(DateTime.UtcNow));
                await Task.Delay(1000);
            }
        }
    }
}
