using CaseExtensions;

namespace Grpc.Testing.Moq.Protobuf;

public class ProtobufType : ProtobufItem
{
    public string Name { get; private set; }

    public ProtobufType(string line)
    {
        Name = line.Replace("(", "").Replace(")", "").Trim();
    }

    public void ResolveName(Dictionary<string, string> namespaceMappings)
    {
        foreach (var namespaceMapping in namespaceMappings)
        {
            if (Name.Contains(namespaceMapping.Key))
            {
                Name = Name.Replace(namespaceMapping.Key, namespaceMapping.Value);
                break;
            }
        }

        var splitted = Name.Split('.');

        for (var i = 0; i < splitted.Length; i++)
        {
            splitted[i] = splitted[i].ToPascalCase();
        }

        Name = string.Join(".", splitted);
        if (Name == "Google.Protobuf.Empty") Name = "Google.Protobuf.WellKnownTypes.Empty";
    }
}
