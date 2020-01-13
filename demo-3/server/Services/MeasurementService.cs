using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Measurement.Net;
using static Measurement.Net.MeasurementService;
using System.Linq;

namespace server
{
    public class MeasurementService : MeasurementServiceBase
    {
        private readonly ILogger<MeasurementService> _logger;
        public MeasurementService(ILogger<MeasurementService> logger)
        {
            _logger = logger;
        }

        public override async Task<MeasurementResult> Calculate(
            IAsyncStreamReader<MeasurementData> requestStream,
            ServerCallContext context)
        {
            var values = new List<double>();

            await foreach (var measurement in requestStream.ReadAllAsync())
            {
                values.Add(measurement.Value);
            }

            return new MeasurementResult
            {
                Count = values.Count,
                Average = CalculateAverage(values),
                Median = CalculateMedian(values)
            };
        }

        public override async Task CalculateStream(
            IAsyncStreamReader<MeasurementData> requestStream,
            IServerStreamWriter<MeasurementResult> responseStream,
            ServerCallContext context)
        {
            var values = new List<double>();

            await foreach (var measurement in requestStream.ReadAllAsync())
            {
                values.Add(measurement.Value);

                var result = new MeasurementResult
                {
                    Count = values.Count,
                    Average = CalculateAverage(values),
                    Median = CalculateMedian(values)
                };

                await responseStream.WriteAsync(result);
            }
        }

        private static double CalculateAverage(IEnumerable<double> values) => values.Average();

        private static double CalculateMedian(IEnumerable<double> values)
        {
            var sorted = values.OrderBy(x => x).ToArray();

            if (sorted.Length % 2 == 0)
            {
                var high = sorted[sorted.Length / 2];
                var low = sorted[(sorted.Length / 2) - 1];
                return (high + low) / 2.0d;
            }
            else
            {
                return sorted[(sorted.Length - 1) / 2];
            }
        }
    }
}
