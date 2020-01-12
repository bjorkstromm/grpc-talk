using System.ServiceModel;
using System.Threading.Tasks;
using ProtoBuf.Grpc;

namespace Greet.Net
{
    [ServiceContract(Name = @"Greeter")]
    public interface IGreeter
    {
        ValueTask<HelloReply> SayHello(HelloRequest request, CallContext context = default);
    }
}
