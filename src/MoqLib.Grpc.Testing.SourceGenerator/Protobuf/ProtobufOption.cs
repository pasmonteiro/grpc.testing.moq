namespace MoqLib.Grpc.Testing.SourceGenerator.Protobuf
{
    public class ProtobufOption : ProtobufItem
    {
        public string Value { get; private set; }

        public ProtobufOption(string line)
        {
            Value = line;
        }
    }
}