namespace Grpc.Testing.Moq.Protobuf;

public class ProtobufNamespace : ProtobufItem
{
    public string Value { get; private set; }

    public ProtobufNamespace(string line)
    {
        Value = line.Replace("package", "").Replace("\"", "").Replace(";", "").Trim();
    }
}
