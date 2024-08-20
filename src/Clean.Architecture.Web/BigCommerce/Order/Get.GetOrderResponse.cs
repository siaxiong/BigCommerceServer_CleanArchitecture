using Clean.Architecture.Core.Entities.BC;

namespace Clean.Architecture.Web.BigCommerce.Order;

public class GetOrderResponse(BC_Order order)
{
  public BC_Order _order { get; set; } = order;
}
