using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.UseCases.Abstractions.Respository;
using MediatR;

namespace Clean.Architecture.Infrastructure.Query;
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
public class GetOrderProductListQueryHandler : IRequestHandler<GetBCOrderProductListQuery, List<BC_OrderProduct>>
{
  private readonly IBigCommerceRepository _bcRepository;
  public GetOrderProductListQueryHandler(IBigCommerceRepository bcRepository)
  {
    _bcRepository = bcRepository;
  }
  public async Task<List<BC_OrderProduct>> Handle(GetBCOrderProductListQuery query, CancellationToken cancellationToken)
  {
    return await _bcRepository.GetBCOrderProductList(query.orderId);
  }
}
public class GetBillingAddressQueryHandler : IRequestHandler<GetBCOrderBillingAddressQuery, BC_BillingAddress>
{
  private readonly IBigCommerceRepository _bcRepository;

  public GetBillingAddressQueryHandler(IBigCommerceRepository bcRepository)
  {
    _bcRepository = bcRepository;
  }

  public async Task<BC_BillingAddress> Handle(GetBCOrderBillingAddressQuery query, CancellationToken  cancellationToken)
  {
    return await _bcRepository.GetBCOrderBillingAddress(query.orderId);
  }
}
public class GetShippingAddressQueryHandler : IRequestHandler<GetBCOrderShippingAddressQuery, BC_ShippingAddress>
{
  private readonly IBigCommerceRepository _bcRepository;

  public GetShippingAddressQueryHandler(IBigCommerceRepository bcRepository)
  {
    _bcRepository = bcRepository;
  }

  public async Task<BC_ShippingAddress> Handle(GetBCOrderShippingAddressQuery query, CancellationToken cancellationToken)
  {
    return await _bcRepository.GetBCOrderShippingAddress(query.orderId);
  }
}
