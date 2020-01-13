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
    }
}
