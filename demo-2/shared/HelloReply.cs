using System.ComponentModel;
using ProtoBuf;

namespace Greet.Net
{
    [ProtoContract]
    public partial class HelloReply
    {
        [ProtoMember(1, Name = @"message")]
        [DefaultValue("")]
        public string Message { get; set; } = "";
    }
}