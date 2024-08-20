using MediatR;
using FastEndpoints;
using Clean.Architecture.Core.Entities.Fishbowl;
using Clean.Architecture.UseCases.Fishbowl.SalesOrder;
using System.Text.Json;


namespace Clean.Architecture.Web.Fishbowl.SalesOrder;

public class Create(IMediator _mediator) : Endpoint<CreateSalesOrderRequest, CreateSalesOrderResponse>
{
  public override void Configure()
  {
    Post(CreateSalesOrderRequest.Route);
    AllowAnonymous();
  }

  public override async Task<CreateSalesOrderResponse> HandleAsync(CreateSalesOrderRequest req, CancellationToken cancellationToken)
  {
    string fB_SO = await _mediator.Send(new CreateSalesOrderCommand(req.orderId));

    Console.WriteLine(fB_SO);
    Response = new CreateSalesOrderResponse(fB_SO);
    return Response;
  }
}
