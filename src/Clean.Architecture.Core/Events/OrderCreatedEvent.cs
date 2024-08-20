using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Core.Interfaces.Events;

namespace Clean.Architecture.Core.Events;
public class OrderCreatedEvent: IEvent
{
  public BC_Order order { get; }

  public OrderCreatedEvent(BC_Order order)
  {
    this.order = order; 
  }
}
