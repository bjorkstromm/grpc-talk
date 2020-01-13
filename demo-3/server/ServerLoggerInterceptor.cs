using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace server
{
    public class ServerLoggerInterceptor : Interceptor
    {
        private readonly ILogger<ServerLoggerInterceptor> _logger;

        public ServerLoggerInterceptor(ILogger<ServerLoggerInterceptor> logger)
        {
            _logger = logger;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var message = new StringBuilder();

            foreach (var header in context.RequestHeaders
                .Where(x => !x.Key.Equals("authorization", StringComparison.OrdinalIgnoreCase)))
            {
                message.AppendLine($"{header.Key}: {header.Value}");
            }
            _logger.LogWarning($"Headers:\n{message.ToString()}");

            var user = context.GetHttpContext().User;

            if (user is object)
            {
                message.Clear();

                foreach (var claim in user.Claims)
                {
                    message.AppendLine($"{claim.Type}: {claim.Value}");
                }
                _logger.LogWarning($"Claims:\n{message.ToString()}");
            }

            return continuation(request, context);
        }
    }
}
