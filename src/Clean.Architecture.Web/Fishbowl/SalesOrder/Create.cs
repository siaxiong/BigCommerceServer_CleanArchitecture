using MediatR;
using FastEndpoints;
using Clean.Architecture.Core.Entities.Fishbowl;
using Clean.Architecture.UseCases.Fishbowl.SalesOrder;
using System.Text.Json;


namespace Clean.Architecture.Web.Fishbowl.SalesOrder;

public class Create(IMediator _mediator) : Endpoint<WebHookPayload, CreateSalesOrderResponse>
{
  public override void Configure()
  {
    Post(CreateSalesOrderRequest.Route);
    AllowAnonymous();
  }

  public override async Task<CreateSalesOrderResponse> HandleAsync(WebHookPayload req, CancellationToken cancellationToken)
  {
    if (req.data is null) throw new Exception("No bigcommerce sales order id!");
    
    string fB_SO = await _mediator.Send(new CreateSalesOrderCommand(req.data.id));

    Response = new CreateSalesOrderResponse(fB_SO);
    return Response;
  }
}
