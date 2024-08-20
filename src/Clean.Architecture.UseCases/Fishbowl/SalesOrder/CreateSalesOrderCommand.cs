using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Fishbowl;
using MediatR;

namespace Clean.Architecture.UseCases.Fishbowl.SalesOrder;
public record CreateSalesOrderCommand(int bigCommerceOrderId) : IRequest<string>;
