using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Time.Net;
using Measurement.Net;
using static Measurement.Net.MeasurementService;
using static Time.Net.TimeService;
using System.Linq;

namespace client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");

            await ServerStreaming(channel);
            await ClientStreaming(channel);
            await BidirectionalStreaming(channel);
        }

        private static async Task ServerStreaming(ChannelBase channel)
        {
            var client =  new TimeServiceClient(channel);
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
        }

        private static async Task ClientStreaming(ChannelBase channel)
        {
            var rand = new Random();
            var client =  new MeasurementServiceClient(channel);
            using var call = client.Calculate();

            for (var i = 0; i < 10; i++)
            {
                var data = new MeasurementData
                {
                    Timestamp = Timestamp.FromDateTimeOffset(DateTimeOffset.Now),
                    Value = rand.NextDouble()
                };

                await call.RequestStream.WriteAsync(data);
            }

            await call.RequestStream.CompleteAsync();
            var response = await call.ResponseAsync;

            Console.WriteLine($"Count : {response.Count}");
            Console.WriteLine($"Average : {response.Average}");
            Console.WriteLine($"Median : {response.Median}");
        }

        private static async Task BidirectionalStreaming(ChannelBase channel)
        {
            var rand = new Random();
            var values = Enumerable.Range(0, 10).Select(x => rand.NextDouble());
            var client =  new MeasurementServiceClient(channel);
            using var call = client.CalculateStream();

            var writeTask = Task.Run(async () =>
            {
                foreach (var value in values)
                {
                    var data = new MeasurementData
                    {
                        Timestamp = Timestamp.FromDateTimeOffset(DateTimeOffset.Now),
                        Value = rand.NextDouble()
                    };

                    await call.RequestStream.WriteAsync(data);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }

                await call.RequestStream.CompleteAsync();
            });

            var readTask = Task.Run(async () =>
            {
                await foreach (var response in call.ResponseStream.ReadAllAsync())
                {
                    Console.WriteLine($"Count : {response.Count}");
                    Console.WriteLine($"Average : {response.Average}");
                    Console.WriteLine($"Median : {response.Median}");
                }
            });

            await Task.WhenAll(new[]
            {
                writeTask,
                readTask
            });
        }
    }
}
