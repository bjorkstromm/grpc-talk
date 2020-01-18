using System;
using System.ComponentModel;
using ProtoBuf;

// bcl types
// https://github.com/protobuf-net/protobuf-net/blob/23b3cac844b9b9cb2cfe95d8c0ae668c7ef06eff/src/protobuf-net.Reflection/protobuf-net/bcl.proto

namespace demo_4
{
    [ProtoContract]
    public class Message
    {
        [ProtoMember(1)]
        public Guid Guid { get; set; } = Guid.Empty;

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public DateTime DateTime { get; set; }

        [ProtoMember(3)]
        public DateTime DateTimeBcl { get; set; }

        [ProtoMember(4)]
        public TimeSpan TimeSpan { get; set; }

        [ProtoMember(5)]
        public decimal Decimal { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var model = ProtoBuf.Meta.RuntimeTypeModel.Create();

            var schema = model.GetSchema(typeof(Message));

            Console.WriteLine(schema);
        }
    }
}
