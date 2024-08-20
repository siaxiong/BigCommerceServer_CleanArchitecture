using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Infrastructure.HttpObjectMapping;

namespace Clean.Architecture.Web.BigCommerce.Customer;

public class GetCustomerResponse(BC_Customer customer)
{
  public BC_Customer customer { get; set; } = customer;
}
