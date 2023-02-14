using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace MoqLib.Grpc.Testing.SourceGenerator.Protobuf
{
    public class ProtobufFile : ProtobufItem
    {
        public AdditionalText additionalFile { get; set; }
        public string Name { get; set; }
        public ProtobufNamespace Namespace { get; set; }
        public ProtobufNamespace CsharpNamespace { get; set; }
        public List<ProtobufOption> Options { get; } = new List<ProtobufOption>();
        public List<ProtobufImport> Imports { get; } = new List<ProtobufImport>();
        public List<ProtobufMessage> Messages { get; } = new List<ProtobufMessage>();
        public List<ProtobufService> Services { get; } = new List<ProtobufService>();
    }
}