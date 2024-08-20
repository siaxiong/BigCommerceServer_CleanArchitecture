using MediatR;
using FastEndpoints;
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Infrastructure.BigCommerce;
using Clean.Architecture.Infrastructure.Respositories;
using Clean.Architecture.UseCases.Abstractions.Respository;


namespace Clean.Architecture.Web.BigCommerce.Order;

public class Get(IBigCommerceRepository bigCommerceRepository) : Endpoint<GetOrderRequest, GetOrderResponse>
{

  public override void Configure()
  {
    Get(GetOrderRequest.Route);
    AllowAnonymous();
  }
      
  public override async Task<GetOrderResponse> HandleAsync(GetOrderRequest orderReq, CancellationToken cancellationToken)
  {
    Console.WriteLine(orderReq.OrderId);
    var resp = await bigCommerceRepository.GetBCOrder(Convert.ToInt32(orderReq.OrderId));
    Console.WriteLine(resp);
    Response = new GetOrderResponse(resp);
    return Response;
  }
}
