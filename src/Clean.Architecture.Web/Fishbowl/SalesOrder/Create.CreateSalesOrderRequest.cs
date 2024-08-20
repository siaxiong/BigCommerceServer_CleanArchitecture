using System.ComponentModel.DataAnnotations;

namespace Clean.Architecture.Web.Fishbowl.SalesOrder;

public class CreateSalesOrderRequest
{
  public const string Route = "/salesorder";

  [Required]
  public int orderId { get; set; }
}
