using System;
using System.Threading.Tasks;
using Greet.Net;
using Grpc.Core;

namespace server
{
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }

    internal class Program
    {
        private const int Port = 50051;

        static async Task Main(string[] args)
        {
            
            // PEM encoded certificate chain + PEM encoded private key
            var certPair = new KeyCertificatePair(Cert, Key);
            var server = new Server
            {
                Services = { Greeter.BindService(new GreeterService()) },
                Ports = { new ServerPort("localhost", Port, new SslServerCredentials(new [] { certPair })) }
            };
            server.Start();

            Console.WriteLine("Greeter server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            await server.ShutdownAsync();
        }

        // CN=localhost
        // Generated with
        // openssl req -newkey rsa:2048 -nodes -x509 -days 365
        private const string Cert = @"
-----BEGIN CERTIFICATE-----
MIIDkzCCAnugAwIBAgIUD82th9NhoaAypOdbTC2K+KdevNAwDQYJKoZIhvcNAQEL
BQAwWTELMAkGA1UEBhMCQVUxEzARBgNVBAgMClNvbWUtU3RhdGUxITAfBgNVBAoM
GEludGVybmV0IFdpZGdpdHMgUHR5IEx0ZDESMBAGA1UEAwwJbG9jYWxob3N0MB4X
DTE5MTAzMTIyMDIyOVoXDTIwMTAzMDIyMDIyOVowWTELMAkGA1UEBhMCQVUxEzAR
BgNVBAgMClNvbWUtU3RhdGUxITAfBgNVBAoMGEludGVybmV0IFdpZGdpdHMgUHR5
IEx0ZDESMBAGA1UEAwwJbG9jYWxob3N0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A
MIIBCgKCAQEArklXjZyKXFHJKhp7Lecj9Mw2K46G9tfe0XZYd7olZdwRcobmMDB1
bRoSaEVM0lKjbtkjpeRt8A+n+OC7CEOOzLNmF9AtyZtAq79Ho/2PWitfkeiqoCLg
6R1xQmZf2Lwk8hN8w907LxId8cAMYUYrdGFJEjJBM2GHC4DOq1ZyMX2nx/E3a5jv
kdZpipJP/rc4hibW9FP5Gv1EZu8kKvoVA5NiPsgXvJrlKvhbtuLcVJH74LFkCaYi
6xsBkZO90a2M1PB2DfhsZs/eAMjQ9hCym9wH3H7pfbP+hUErPkr/uGNhPzPa0sB0
yDmhVF6F6+HuuKYeQSqE/CVUYnu4VuYBkwIDAQABo1MwUTAdBgNVHQ4EFgQUyDjd
ydko9/IkZQW4UU0QcTaEmkgwHwYDVR0jBBgwFoAUyDjdydko9/IkZQW4UU0QcTaE
mkgwDwYDVR0TAQH/BAUwAwEB/zANBgkqhkiG9w0BAQsFAAOCAQEAb2XlgpwTnKcj
faGKajocUDwYW+kAf/VzcuS2JVLw2wuSHT5KsIsiCuMAnbcaxsZAKBXoIBHj9s2W
9xSTbphA+DwUKl28jtqcOsWZfhPfaeuG+2G7BFoseIDNGzHyQl6bW+2zO0Mz4unU
mPNNSj4UQObkS3VI0zeZCRIjTCjPWdgUA1Y4NASDGsq4mpeQiD5H6AMynL22KpZB
uf0MA8f5Ow9PPncjWpFg3c6naOD7qsuii0+wc9RKduoR80LmFh9BxyLoQUNgXhWg
hJaMgJ0hVKrupyfMo05sb4Tq03QgXRiUVb94AFi/TLnF2UiOfQym0kizXxgcqPqA
NHQ6OFRp8g==
-----END CERTIFICATE-----";

            private const string Key = @"
-----BEGIN PRIVATE KEY-----
MIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCuSVeNnIpcUckq
Gnst5yP0zDYrjob2197Rdlh3uiVl3BFyhuYwMHVtGhJoRUzSUqNu2SOl5G3wD6f4
4LsIQ47Ms2YX0C3Jm0Crv0ej/Y9aK1+R6KqgIuDpHXFCZl/YvCTyE3zD3TsvEh3x
wAxhRit0YUkSMkEzYYcLgM6rVnIxfafH8TdrmO+R1mmKkk/+tziGJtb0U/ka/URm
7yQq+hUDk2I+yBe8muUq+Fu24txUkfvgsWQJpiLrGwGRk73RrYzU8HYN+Gxmz94A
yND2ELKb3Afcful9s/6FQSs+Sv+4Y2E/M9rSwHTIOaFUXoXr4e64ph5BKoT8JVRi
e7hW5gGTAgMBAAECggEAMdmGclm49WjrKeDDJMD89tLGF7U/gzEBaVX5Pd1/PuAw
GVreUiA6JgeUBkD0FSBP4K+404U/sK85syTMOjPgj0osxFjmD8kZOUaPvicTLo5D
Wp961izDucmTEJPpjDtcMeVDBV6sw+zhpbjxkY0ZdsMwvESlg4W8s5yXAEjMhrzZ
+hxdZLtU5gNbF6IweX2Kxchaxpe3VfD+J4yrNIVO1rAJo0pBEi6gmBZlMB1ykkhX
QiZo0napxwR4LnaZ/kFQULgrbpH4xrpvJfKjOTOR1aUFMgoRMF9SrALQTFOjbPvk
DC9wWvx+CvJHurCiLSOffcUXLwrWA51cuFfQA8XRIQKBgQDh7ZsQWPueECXC57Sl
JNw+hHQAZjF3dyz8S0wACg5XCcrV11+IHakAVAwBAOV754HOH1BCeq93XtC0t2rE
TCfMxg8FmBmkVnw11o60Ba7ItsPWY/UstXkIcEVYC1asjfyfMC8sAIAEe/ty+WM7
gOEH4WVeBdBtgQDFKLUGUz9yEQKBgQDFfBJIw13qAmPQuiWt3CLICAUgniJQ8Zzn
H1O6qrZ2GV+oj2XFIviTQbURMD0CQE98LkHiYg2D0wCvt0h5oD62ZvYkJhgmVAyB
eDKV8SZsYmY0Pg4nAAaWDiKJzfZA/kx3yn6eX1uPnvVyzffn1NWLvlIthtxu4SfA
KaPWf2mVYwKBgHE7MrI4xrLriOFsW39BQBkdLT5d+YDUe/lei5Khd5prz/ro3HpN
0zvU48dE+b7lworZ6tHGx1ZecN+B5cLIIFJjGXhGSOOybMJW7GKRTo5N+0ziACkO
MsDl8/syg5gr+WaXMa0t+vtEYDu5feB+sUnPz4wWAeY/93DY/BFVe8DhAoGAOJr8
nrCpHac7txt0K2Z67b0mwxewnGT05WacWFgQXr+dJLKdqCkC+SqPXPLnudp3LhAQ
QHR8jTmQ68zsUDh3YU8X5Hqq12mmAAJU9ZeevNhjlTvoUJN4T9CTY86OdEiv4HJB
YuHQW9VjY1nYYjImV5zg7z3Ft16AR1toI9Z3jCMCgYBbRm4G+8fL9+0Sm3fu7gvV
fwuSGj6RdAoy+ug49Utjf/RKeab5T4H1J5txy+P+/I6z+6O4UTXuDuQxCm0I0riF
eCtsayb5SLti7lthLnl+3Y8c3D1Nq6sfAzBrw6g1AsYRxL9qP3aDO7cRLN5L/hkv
rZ89c32B4pwMTBMmBN90vA==
-----END PRIVATE KEY-----";
    }
}
