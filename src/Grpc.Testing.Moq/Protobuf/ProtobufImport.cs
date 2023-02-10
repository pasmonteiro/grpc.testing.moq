namespace Grpc.Testing.Moq.Protobuf;

public class ProtobufImport : ProtobufItem
{
    public string Value { get; private set; }

    public ProtobufImport(string line)
    {
        Value = line;
    }
}
