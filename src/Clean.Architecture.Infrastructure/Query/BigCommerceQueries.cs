using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC;
using MediatR;

namespace Clean.Architecture.Infrastructure.Query;

public record GetBCOrderQuery(int orderId):IRequest<BC_Order>;
public record GetBCOrderProductListQuery(int orderId):IRequest<List<BC_OrderProduct>>;
public record GetBCOrderBillingAddressQuery(int orderId):IRequest<BC_BillingAddress>;
public record GetBCOrderShippingAddressQuery(int orderId):IRequest<BC_ShippingAddress>;
