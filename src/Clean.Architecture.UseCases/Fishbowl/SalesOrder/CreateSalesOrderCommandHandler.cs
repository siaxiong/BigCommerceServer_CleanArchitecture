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

namespace Clean.Architecture.UseCases.Fishbowl.SalesOrder;
public class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, string>
{
  private readonly IBigCommerceRepository _bigCommerceRepository;
  private readonly IFishbowlRespository _fishbowlRespository;

  public CreateSalesOrderCommandHandler(IBigCommerceRepository bigCommerceRepository, IFishbowlRespository fishbowlRespository)
  {
   _bigCommerceRepository = bigCommerceRepository;
   _fishbowlRespository = fishbowlRespository;
  }
  public async Task<string> Handle(CreateSalesOrderCommand command, CancellationToken cancellationToken)
  {
    BC_Order bc_Order = await _bigCommerceRepository.GetBCOrder(command.bigCommerceOrderId);
    BC_Customer bc_Customer = await _bigCommerceRepository.GetBCCustomer(bc_Order.customer_id);

    List<FB_SOItem> fb_soItemList = new List<FB_SOItem>();

    FB_BillingAddress fb_BillingAddress = new FB_BillingAddress(bc_Order.billing_address.first_name + " " + bc_Order.billing_address.last_name,
    bc_Order.billing_address.street_1, bc_Order.billing_address.city, bc_Order.billing_address.state, bc_Order.billing_address.zipcode, bc_Order.billing_address.country);

    FB_ShippingAddress fb_ShippingAddress = new FB_ShippingAddress((bc_Order.b2CShippingAddress.first_name +" "+ bc_Order.b2CShippingAddress.last_name),bc_Order.b2CShippingAddress.street_1,
    bc_Order.b2CShippingAddress.city, bc_Order.b2CShippingAddress.state, bc_Order.b2CShippingAddress.zipcode, bc_Order.b2CShippingAddress.country);

    foreach (BC_OrderProduct product in bc_Order.products)
    {
      fb_soItemList.Add(new FB_SOItem(FB_SOItem.SOItemType.Sale, product.id.ToString(), product.sku,
        product.name, Convert.ToDouble(product.base_price), product.quantity,
        "ea", Convert.ToDouble(product.discountAmount)));
    }

    List<string> SOHeaderList = new List<string>();

    SOHeaderList.Add("SO");
    SOHeaderList.Add($"TEST_{command.bigCommerceOrderId}");
    SOHeaderList.Add("10");
    SOHeaderList.Add($"{bc_Customer.company}");
    SOHeaderList.Add($"{bc_Customer.first_name} {bc_Customer.last_name}");
    SOHeaderList.Add($"{fb_BillingAddress.billToName}");
    SOHeaderList.Add($"{((fb_BillingAddress.street_2) != null ? (fb_BillingAddress.street_1 + " " + fb_BillingAddress.street_2) : fb_BillingAddress.street_1)}");
    SOHeaderList.Add($"{fb_BillingAddress.city}");
    SOHeaderList.Add($"{fb_BillingAddress.state}");
    SOHeaderList.Add($"{fb_BillingAddress.zipcode}");
    SOHeaderList.Add($"{fb_BillingAddress.country}");
    SOHeaderList.Add($"{fb_ShippingAddress.ShipToName}");
    SOHeaderList.Add($"{((fb_ShippingAddress.street_2) != null ? (fb_ShippingAddress.street_1 + "" + fb_ShippingAddress.street_2): fb_ShippingAddress.street_1)}");
    SOHeaderList.Add($"{fb_ShippingAddress.city}");
    SOHeaderList.Add($"{fb_ShippingAddress.state}");
    SOHeaderList.Add($"{fb_ShippingAddress.zipcode}");
    SOHeaderList.Add($"{fb_ShippingAddress.country}");
    SOHeaderList.Add($"false,");
    SOHeaderList.Add($"UPS,");
    SOHeaderList.Add($"None");

    for(int i = 0; i < SOHeaderList.Count; i++)
    {
      SOHeaderList[i] = SOHeaderList[i].Trim(new char[] {','});
    }

    List<string[]> OrderItemList = new List<string[]>();
/*
    OrderItemList.Add(new string[] { "Flag", "SONum", "Status", "CustomerName", "CustomerContact", "BillToName", "BillToAddress", "BillToCity", "BillToState", "BillToZip", "BillToCountry", "ShipToName", "ShipToAddress", "ShipToCity", "ShipToState", "ShipToZip", "ShipToCountry", "ShipToResidential", "CarrierName", "TaxRateName" });
*/
    OrderItemList.Add(SOHeaderList.ToArray());

    foreach(FB_SOItem item in fb_soItemList)
    {
      OrderItemList.Add(new string[]{"Item","10", $"{item.ProductNumber}",
      $"{item.ProductDescription}",$"{item.ProductQuantity}",
      $"ea",$"{item.ProductPrice}" });
    }

    Console.WriteLine("*** START ***");
    Console.WriteLine(JsonSerializer.Serialize(OrderItemList));
    Console.WriteLine(JsonSerializer.Serialize(new string[] { "Item", "10" }));
    Console.WriteLine("*** END ***");
    var temp = new List<string[]>();

    await _fishbowlRespository.CreateFBSO(JsonSerializer.Serialize(OrderItemList));
    return JsonSerializer.Serialize(OrderItemList);


  }
}
