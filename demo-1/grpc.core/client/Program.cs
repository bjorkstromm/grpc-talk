using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Greet.Net;
using Grpc.Core;

namespace client
{
    class Program
    {
        private static string GetCertificate(string host, int port)
        {
            using var client = new TcpClient();
            client.Connect(host, port);

            using var ssl = new SslStream(client.GetStream(), false, (s, x, y, z) => true, null);

            try
            {
                ssl.AuthenticateAsClient(host);
            }
            catch (Exception)
            {
                ssl.Close();
                client.Close();
                return string.Empty;
            }

            using var cert = new X509Certificate2(ssl.RemoteCertificate);
            ssl.Close();
            client.Close();

            var builder = new StringBuilder();
            builder.AppendLine("-----BEGIN CERTIFICATE-----");
            builder.AppendLine(Convert.ToBase64String(cert.RawData, Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END CERTIFICATE-----");

            return builder.ToString();
        }

        static async Task Main(string[] args)
        {
            // You should not do this. Or you should at least validate the certificate.
            var certificateRoot = GetCertificate("localhost", 50051);

            // Trusted root certificates in PEM format. No integration with Certificate Store. :´(
            var channel = new Channel("localhost:50051", new SslCredentials(certificateRoot));
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "gRPC Core" });

            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
