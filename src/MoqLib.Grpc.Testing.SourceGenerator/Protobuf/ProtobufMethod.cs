namespace MoqLib.Grpc.Testing.SourceGenerator.Protobuf
{
    public class ProtobufMethod : ProtobufItem
    {
        public string Name { get; private set; }
        public ProtobufType Input { get; private set; }
        public ProtobufType Output { get; private set; }
        public bool IsStream { get; private set; }

        public ProtobufMethod(string line)
        {
            var lineItems = line.Split(' ');
            var split = lineItems[1].Split('(');
            Name = split[0];
            IsStream = line.Contains("stream");
            Input = new ProtobufType(split[1]);
            Output = new ProtobufType(IsStream ? lineItems[4] : lineItems[3]);
        }
    }
}