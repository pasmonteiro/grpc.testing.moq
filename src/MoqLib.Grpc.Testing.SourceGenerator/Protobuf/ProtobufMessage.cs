namespace MoqLib.Grpc.Testing.SourceGenerator.Protobuf;

public class ProtobufMessage : ProtobufItem
{
    public string Name { get; set; }
    public List<ProtobufField> Fields { get; set; }
}
