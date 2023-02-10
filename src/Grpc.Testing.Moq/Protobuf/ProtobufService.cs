namespace Grpc.Testing.Moq.Protobuf;

public class ProtobufService : ProtobufItem
{
    public string Name { get; private set; }
    public List<ProtobufMethod> Methods { get; private set; } = new List<ProtobufMethod>();

    public ProtobufService(string line, StreamReader reader)
    {
        var lineItems = line.Split(' ');
        Name = lineItems[1];

        bool isCurlyBracesOpened = line.Trim().EndsWith("{");
        while (!reader.EndOfStream)
        {
            var cLine = reader.ReadLine();
            var trimLine = cLine?.Trim();
            if (string.IsNullOrEmpty(trimLine)) continue;
            if (trimLine.StartsWith("}") == true) break;
            if (trimLine.StartsWith("{"))
            {
                isCurlyBracesOpened = true;
                continue;
            }
            if (trimLine.StartsWith("rpc"))
            {
                Methods.Add(new ProtobufMethod(trimLine));
                continue;
            }
        }
    }
}
