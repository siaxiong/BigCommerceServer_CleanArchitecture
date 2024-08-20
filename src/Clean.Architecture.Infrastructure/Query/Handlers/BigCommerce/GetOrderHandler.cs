using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.UseCases.Abstractions.Respository;
using Clean.Architecture.Infrastructure.HttpObjectMapping;
using Clean.Architecture.Infrastructure.Query.BigCommerce;
using MailKit.Search;
using MediatR;

namespace Clean.Architecture.Infrastructure.Query.Handlers.BigCommerce;
public class GetOrderHandler : IRequestHandler<GetBCOrderQuery, BC_Order>
{
  private readonly IBigCommerceRepository _bcRepository;
  public GetOrderHandler(IBigCommerceRepository bcRespository)
  {
    _bcRepository = bcRespository;
  }
  public async Task<BC_Order> Handle(GetBCOrderQuery request, CancellationToken cancellationToken)
  {
    return await _bcRepository.GetBCOrder(request.orderId);
  }

}
