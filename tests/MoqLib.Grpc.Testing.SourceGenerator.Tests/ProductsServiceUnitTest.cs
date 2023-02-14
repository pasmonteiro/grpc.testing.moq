using MyCompany.Grpc.Services.Mocks;
using MyCompany.Grpc.Services;
using Xunit;

namespace MoqLib.Grpc.Testing.SourceGenerator.Tests;

public class ProductsServiceUnitTests
{
    [Fact]
    public void Test1()
    {
        ProductsServiceClientMock.GenerateMock().SetupGetCall(request =>
        {
            // Assert Request

            return new GetProductResponseMsg();
        });
    }
}