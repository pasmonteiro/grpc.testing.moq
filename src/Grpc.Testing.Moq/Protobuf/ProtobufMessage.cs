namespace Grpc.Testing.Moq.Protobuf;

public class ProtobufMessage : ProtobufItem
{
    public string Name { get; set; }
    public List<ProtobufField> Fields { get; set; }
}
