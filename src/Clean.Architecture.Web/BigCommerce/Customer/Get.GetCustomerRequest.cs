using System.ComponentModel.DataAnnotations;

namespace Clean.Architecture.Web.BigCommerce.Customer;

public class GetCustomerRequest
{
  public const string Route = "/customer";

  [Required]
  public string? CustomerId { get; set; }
}
