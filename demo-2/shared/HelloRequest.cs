using System.ComponentModel;
using ProtoBuf;

namespace Greet.Net
{
    [ProtoContract]
    public partial class HelloRequest
    {
        [ProtoMember(1, Name = @"name")]
        [DefaultValue("")]
        public string Name { get; set; } = "";
    }
}