using System.Reflection;
using System.Text;
using MoqLib.Grpc.Testing.SourceGenerator.Protobuf;
using HandlebarsDotNet;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace MoqLib.Grpc.Testing.SourceGenerator;

[Generator]
public class ProtobufMoqSourceGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var template = ResourceReader.GetResource("Class.handlebars");

        var files = context.AdditionalFiles.ToList();
        if (!files.Any())
        {
            LogWarning(context, "CA002", "No AdditionalFiles found", "Please ensure AdditionalFiles are present in your .csproj file.", Helper.GetDiagnosticLocation());
            return;
        }

        var protobufFiles = new List<ProtobufFile>();
        var namespaceDictionary = new Dictionary<string, string>();

        foreach (var file in files)
        {
            try
            {
                ProtobufFile protobufFile;

                using (var stream = new FileStream(file.Path, FileMode.Open))
                using (var reader = new StreamReader(stream))
                    protobufFile = ProtobufFileInfo.Read(file, reader);

                if (protobufFile is not null)
                {
                    protobufFiles.Add(protobufFile);
                    if (!string.IsNullOrEmpty(protobufFile.Namespace?.Value) && !string.IsNullOrEmpty(protobufFile.CsharpNamespace?.Value)
                        && !namespaceDictionary.ContainsKey(protobufFile.Namespace.Value))
                    {
                        namespaceDictionary.Add(protobufFile.Namespace.Value, protobufFile.CsharpNamespace.Value);
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogWarning(context, "CA003", "Mock generation", $"An error occured while reading for {file.Path}. Exception message: {ex.Message}", Helper.GetDiagnosticLocation());
            }
        }

        foreach (var protobufFile in protobufFiles)
        {
            try
            {
                var service = protobufFile.Services.FirstOrDefault();

                if (service is null)
                {
                    LogWarning(context, "CA005", "Mock generation", $"No services found in {protobufFile.additionalFile.Path}.", Helper.GetDiagnosticLocation());
                    continue;
                }

                service.Methods.ForEach(_ =>
                {
                    _.Input.ResolveName(namespaceDictionary);
                    _.Output.ResolveName(namespaceDictionary);
                });

                var className = $"{service.Name}ClientMock";
                var grpcClient = $"{service.Name}.{service.Name}Client";
                var generatedClass = Handlebars.Compile(template)(new
                {
                    usingNamespace = protobufFile.CsharpNamespace.Value,
                    namespaceName = $"{protobufFile.CsharpNamespace.Value}.Mocks",
                    className,
                    grpcClient,
                    methods = service.Methods.Select(_ => new
                    {
                        name = _.Name,
                        input = _.Input,
                        output = _.Output,
                        isStream = _.IsStream,
                        grpcClient
                    }),
                    outputTypes = (
                        from _ in service.Methods
                        group _ by _.Output.Name into newGroup
                        select new
                        {
                            input = newGroup.First().Input,
                            output = newGroup.First().Output,
                            isStream = newGroup.First().IsStream
                        }
                    ),
                    toolName = Assembly.GetExecutingAssembly().GetName().Name,
                    toolVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString()
                });

                context.AddSource($"{className}.g.cs", SourceText.From(generatedClass, Encoding.UTF8));
            }
            catch (System.Exception ex)
            {
                LogWarning(context, "CA004", "Mock generation", $"An error occured while generating for {protobufFile.additionalFile.Path}. Exception message: {ex.Message}", Helper.GetDiagnosticLocation());
            }
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }

    static void LogWarning(GeneratorExecutionContext context, string id, string title, string message, Location location)
    {
        context.ReportDiagnostic(Diagnostic.Create(
                new DiagnosticDescriptor(id, title, message, "Proto", DiagnosticSeverity.Warning, true),
                location
            ));
    }
}
