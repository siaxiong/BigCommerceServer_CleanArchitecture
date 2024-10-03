using System.ComponentModel.DataAnnotations;

namespace Clean.Architecture.Web.Fishbowl.SalesOrder;


public class WebHookPayload
{
  public CreateSalesOrderRequest? data { get; set; }
}
public class CreateSalesOrderRequest
{
  public const string Route = "/salesorder";

  [Required]
  public int id { get; set; }
}
