using Grpc.Core;
using Grpc.Net.Client;
using MyCompany.Grpc.Services;

namespace SampleProject;

public class MyService
{
    private readonly OrdersService.OrdersServiceClient _ordersServiceClient;
    public MyService(OrdersService.OrdersServiceClient ordersServiceClient)
        => _ordersServiceClient = ordersServiceClient;
    public async Task<List<OrderMsg>> FindActiveOrdersAsync()
    {
        var response = _ordersServiceClient.Search(new SearchOrdersMsg
        {
            Active = true
        });
        
        var orders = new List<OrderMsg>();
        await foreach (var item in response.ResponseStream.ReadAllAsync())
            orders.Add(item);
        
        return orders;
    }
}