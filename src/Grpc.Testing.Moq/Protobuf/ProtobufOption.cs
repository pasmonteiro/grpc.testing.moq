namespace Grpc.Testing.Moq.Protobuf;

public class ProtobufOption : ProtobufItem
{
    public string Value { get; private set; }

    public ProtobufOption(string line)
    {
        Value = line;
    }
}
