using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Greet.Net;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using static Greet.Net.Greeter;

namespace server
{
    [Authorize]
    public class GreeterService : GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(Empty request, ServerCallContext context)
        {
            var user = context.GetHttpContext().User;

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + user.Identity.Name
            });
        }
    }
}
