using Microsoft.CodeAnalysis;

namespace Grpc.Testing.Moq.Protobuf;

public class ProtobufFileInfo
{
    public static ProtobufFile Read(AdditionalText additionalFile, StreamReader reader)
    {
        var file = new ProtobufFile();
        file.additionalFile = additionalFile;

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var trimLine = line?.Trim();

            if (string.IsNullOrEmpty(trimLine)) continue;
            if (trimLine.StartsWith("import"))
            {
                file.Imports.Add(new ProtobufImport(trimLine));
                continue;
            }
            if (trimLine.StartsWith("service"))
            {
                file.Services.Add(new ProtobufService(trimLine, reader));
                continue;
            }
            if (trimLine.StartsWith("package"))
            {
                file.Namespace = new ProtobufNamespace(trimLine);
                continue;
            }
            if (trimLine.StartsWith("option csharp_namespace"))
            {
                file.CsharpNamespace = new ProtobufNamespace(trimLine
                    .Replace("option csharp_namespace", "")
                    .Replace("=", "")
                    .Trim()
                );
                continue;
            }
        }

        return file;
    }
}
