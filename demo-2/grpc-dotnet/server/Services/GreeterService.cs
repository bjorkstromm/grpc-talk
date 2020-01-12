using System.Threading.Tasks;
using Greet.Net;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc;

namespace server
{
    public class GreeterService : IGreeter
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public ValueTask<HelloReply> SayHello(HelloRequest request, CallContext context = default)
        {
            _logger.LogInformation($"Hello from {context.ServerCallContext.Method}");

            return new ValueTask<HelloReply>(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
