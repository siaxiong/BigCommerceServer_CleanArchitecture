using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC;

namespace Clean.Architecture.Core.Interfaces;
public interface IBCService
{
  public BC_Order CreateB2COrder(int id);
  public BC_ShippingAddress ExtractB2COrderShippingAddress(int orderId);
  public BC_OrderProduct ExtractGetB2COrderProduct(int orderId);
  public BC_Customer ExtractB2CCustomer(int customerId);

  public Task<BC_Order> GetB2COrder(int orderId);

}
