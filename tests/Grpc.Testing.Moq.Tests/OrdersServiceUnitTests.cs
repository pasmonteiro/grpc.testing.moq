using MyCompany.Grpc.Services.Mocks;
using MyCompany.Grpc.Services;

namespace Grpc.Testing.Moq.Tests;

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