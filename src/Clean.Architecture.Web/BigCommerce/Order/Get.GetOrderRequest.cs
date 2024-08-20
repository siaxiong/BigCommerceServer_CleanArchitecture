using System.ComponentModel.DataAnnotations;

namespace Clean.Architecture.Web.BigCommerce.Order;

public class GetOrderRequest
{
  public const string Route = "/order";

  [Required]
  public string? OrderId { get; set; }
}
