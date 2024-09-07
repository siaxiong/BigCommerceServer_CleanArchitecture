using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC;

namespace Clean.Architecture.UseCases.Abstractions.Respository;
public interface IBigCommerceRepository
{
  Task<BC_Order> GetBCOrder(int orderId);
  Task<List<BC_OrderProduct>> GetBCOrderProductList(int orderId);
  Task<BC_BillingAddress> GetBCOrderBillingAddress(int orderId);
  Task<BC_ShippingAddress> GetBCOrderShippingAddress(int orderId);
  Task<int> GetB2BCustomerId(int b2cOrderId);
  Task<BC_Customer> GetBCCustomer(int customerId);
  Task ArchiveOrder(int orderId);
  Task ChangeOrderStatus(int orderId);

  Task UpdateCompanyCredits(int companyId, double credits);
/*  Task<string> GetCustomerEmail(int customerId);
*/
}
