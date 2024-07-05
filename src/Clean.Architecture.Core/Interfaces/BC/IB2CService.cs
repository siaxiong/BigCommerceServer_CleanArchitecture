using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC.B2C;

namespace Clean.Architecture.Core.Interfaces.BC;
public interface IB2CService
{
/*  public B2COrder ExtractB2COrder(int id);
*/  public B2CBillingAddress ExtractB2COrderBillingAddress(int orderId);
/*  public B2CShippingAddress ExtractB2COrderShippingAddress(int orderId);
  public B2COrderProduct ExtractGetB2COrderProduct(int orderId);
  public B2CCustomer ExtractB2CCustomer(int customerId);*/

}
