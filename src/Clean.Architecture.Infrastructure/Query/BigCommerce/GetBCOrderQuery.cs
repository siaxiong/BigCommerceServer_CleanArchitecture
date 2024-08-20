using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC;
using MediatR;

namespace Clean.Architecture.Infrastructure.Query.BigCommerce;
public record GetBCOrderQuery(int orderId):IRequest<BC_Order>;
