using Clean.Architecture.Infrastructure.HttpObjectMapping;
using Clean.Architecture.UseCases.Abstractions.Respository;
using FastEndpoints;
using MediatR;

namespace Clean.Architecture.Web.BigCommerce.Customer;


public class Get(IBigCommerceRepository bigCommerceRepository) : Endpoint<GetCustomerRequest, GetCustomerResponse>
{
  public override void Configure()
  {
    Get(GetCustomerRequest.Route);
    AllowAnonymous();
  }

  public override async Task<GetCustomerResponse> HandleAsync(GetCustomerRequest request, CancellationToken cancellationToken)
  {
    var resp = await bigCommerceRepository.GetBCCustomer(Convert.ToInt32(request.CustomerId));
    Response = new GetCustomerResponse(resp);
    return Response;
  }

}
