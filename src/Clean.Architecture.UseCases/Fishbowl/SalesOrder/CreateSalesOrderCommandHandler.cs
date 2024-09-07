using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Core.Entities.Fishbowl;
using Clean.Architecture.UseCases.Abstractions;
using Clean.Architecture.UseCases.Abstractions.Respository;
using MediatR;
using Clean.Architecture.UseCases.DTO;

namespace Clean.Architecture.UseCases.Fishbowl.SalesOrder;
public class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, string>
{
  private readonly IBigCommerceRepository _bigCommerceRepository;
  private readonly IFishbowlRespository _fishbowlRespository;
  FishbowlDTO fbDTO = new FishbowlDTO();

  public CreateSalesOrderCommandHandler(IBigCommerceRepository bigCommerceRepository, IFishbowlRespository fishbowlRespository)
  {
   _bigCommerceRepository = bigCommerceRepository;
   _fishbowlRespository = fishbowlRespository;
  }
  public async Task<string> Handle(CreateSalesOrderCommand command, CancellationToken cancellationToken)
  {
    BC_Order bc_Order = await _bigCommerceRepository.GetBCOrder(command.bigCommerceOrderId);
    BC_Customer bc_Customer = await _bigCommerceRepository.GetBCCustomer(bc_Order.customer_id);

    double customerCredit = await _fishbowlRespository.GetFbCustomerCredit(bc_Customer.company);
    if ((Convert.ToDouble(bc_Order.subtotal_inc_tax) + customerCredit) > 0)
    {
      Console.WriteLine("***INSUFFICIENT FUNDS!***");
      await _bigCommerceRepository.ArchiveOrder(command.bigCommerceOrderId);
      await _bigCommerceRepository.ChangeOrderStatus(command.bigCommerceOrderId);
      throw new Exception("Insufficient funds!");
    }
    List<FB_SOItem> fb_soItemList = fbDTO.SOItemListDTO(bc_Order.products);
    FB_BillingAddress fb_BillingAddress = fbDTO.BillingAddressDTO(bc_Order.billing_address);
    FB_ShippingAddress fb_ShippingAddress = fbDTO.ShippingAddressDTO(bc_Order.b2CShippingAddress);
    String OrderItemList = fbDTO.CSVOrderDTO(fb_BillingAddress, fb_ShippingAddress, bc_Customer,fb_soItemList,  command.bigCommerceOrderId);

    await _fishbowlRespository.CreateFBSO(OrderItemList);
    await _bigCommerceRepository.UpdateCompanyCredits(await _bigCommerceRepository.GetB2BCustomerId(command.bigCommerceOrderId), customerCredit);
    return OrderItemList;
  }
}
