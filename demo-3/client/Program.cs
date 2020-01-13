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
using Grpc.Core.Interceptors;
using System.Net.Http;
using System.Web;
using static Greet.Net.Greeter;

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

            await AuthenticatedUnaryCall();
        }

        private static async Task ServerStreaming(ChannelBase channel)
        {
            var client =  new TimeServiceClient(channel);
            using var reply = client.Subscribe(new SubscribeRequest
            {
                Interval = 1,
                Count = 5,
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
            var values = Enumerable.Range(0, 5).Select(x => rand.NextDouble());
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

        private static async Task AuthenticatedUnaryCall()
        {
            var address = "https://localhost:5001";
            var token = await Authenticate(address);

            using var channel = GrpcChannel.ForAddress(address);
            var invoker = channel.Intercept(metadata =>
            {
                metadata.Add("Authorization", $"Bearer {token}");
                return metadata;
            });

            var client = new GreeterClient(invoker);

            var response = await client.SayHelloAsync(new Empty());

            Console.WriteLine(response.Message);
        }

        private static async Task<string> Authenticate(string address)
        {
            using var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{address}/generateJwtToken?name={HttpUtility.UrlEncode(Environment.UserName)}"),
                Method = HttpMethod.Get,
                Version = new Version(2, 0)
            };

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
