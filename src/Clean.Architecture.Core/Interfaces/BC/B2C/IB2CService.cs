using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC.B2C;

namespace Clean.Architecture.Core.Interfaces.BC.B2C;
public interface IB2CService
{
  public B2CBillingAddress GetB2COrderBillingAddress(int orderId);
}
