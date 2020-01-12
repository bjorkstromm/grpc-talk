using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Time.Net;
using static Time.Net.TimeService;

namespace server
{
    public class TimeService : TimeServiceBase
    {
        private readonly ILogger<TimeService> _logger;
        public TimeService(ILogger<TimeService> logger)
        {
            _logger = logger;
        }

        public override async Task Subscribe(
            SubscribeRequest request,
            IServerStreamWriter<Timestamp> responseStream,
            ServerCallContext context)
        {
            var timeout = request.Unit switch
            {
                SubscribeRequest.Types.Unit.Seconds => TimeSpan.FromSeconds(request.Interval),
                _ => TimeSpan.FromMilliseconds(request.Interval)
            };

            for (var i = 0; i < request.Count; i++)
            {
                var dateTime = DateTime.UtcNow;
                var timeStamp = Timestamp.FromDateTime(dateTime);
                await responseStream.WriteAsync(timeStamp);

                await Task.Delay(timeout);
            }
        }

        public override async Task<SyncResponse> Sync(
            IAsyncStreamReader<Timestamp> requestStream,
            ServerCallContext context)
        {
            var count = 0;
            var sw = new Stopwatch();
            sw.Start();

            await foreach (var timeStamp in requestStream.ReadAllAsync())
            {
                // noop;
            }

            sw.Stop();

            var message = $"You sent {count} timestamps over {sw.ElapsedMilliseconds}ms";

            return new SyncResponse
            {
                Message = message
            };
        }

        public override async Task Ping(
            IAsyncStreamReader<Timestamp> requestStream,
            IServerStreamWriter<Int32Value> responseStream,
            ServerCallContext context)
        {
            await foreach (var timeStamp in requestStream.ReadAllAsync())
            {
                var now = DateTime.UtcNow;
                var diff = now - timeStamp.ToDateTime();

                await responseStream.WriteAsync(new Int32Value
                { 
                    Value = diff.Milliseconds
                });
            }
        }
    }
}
