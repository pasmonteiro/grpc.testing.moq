using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace MoqLib.Grpc.Testing.SourceGenerator;

internal static class Helper
{
    public static Location GetDiagnosticLocation([CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        => Location.Create(sourceFilePath, new TextSpan(), new LinePositionSpan(new LinePosition(sourceLineNumber, 0), new LinePosition(sourceLineNumber, 0)));
}
