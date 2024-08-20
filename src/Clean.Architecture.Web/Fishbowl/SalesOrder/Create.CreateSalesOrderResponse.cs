using Clean.Architecture.Core.Entities.Fishbowl;

namespace Clean.Architecture.Web.Fishbowl.SalesOrder;

public class CreateSalesOrderResponse(string so)
{
  public string _fB_SO { get; set; } = so;
}
