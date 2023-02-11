using MyCompany.Grpc.Services;
using MyCompany.Grpc.Services.Mocks;
using Xunit;

namespace SampleProject.Tests;

public class MyServiceUnitTest
{
    [Fact]
    public async Task TestFindActiveOrdersAsync()
    {
        // Arrange
        var orderMock = OrdersServiceClientMock.GenerateMock();
        orderMock.SetupSearchCall(request =>
        {
            // Assert
            Assert.True(request.Active);

            return new OrderMsg[]
            {
                new OrderMsg
                {
                    Id = 1,
                    Name = "The Order"
                }
            };
        });
        var service = new MyService(orderMock.Object);

        // Act
        var orders = await service.FindActiveOrdersAsync();

        // Assert
        Assert.NotEmpty(orders);
    }
}