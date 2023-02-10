using MyCompany.Grpc.Services.Mocks;
using MyCompany.Grpc.Services;

namespace Grpc.Testing.Moq.Tests;

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