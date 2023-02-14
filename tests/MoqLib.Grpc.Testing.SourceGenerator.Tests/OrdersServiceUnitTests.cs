using MyCompany.Grpc.Services.Mocks;
using MyCompany.Grpc.Services;
using Xunit;

namespace MoqLib.Grpc.Testing.SourceGenerator.Tests;

public class OrdersServiceUnitTests
{
    [Fact]
    public void Test1()
    {
        OrdersServiceClientMock.GenerateMock().SetupGetCall(request =>
        {
            // Assert Request

            return new GetOrderResponseMsg();
        });
    }
}