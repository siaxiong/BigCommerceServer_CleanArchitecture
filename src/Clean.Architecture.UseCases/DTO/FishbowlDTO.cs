using System.Reflection;
using System.Text.Json;
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Core.Entities.Fishbowl;

namespace Clean.Architecture.UseCases.DTO;

/**
 * Converting Big Commerce orders to Fishbowl orders
 */
public class FishbowlDTO
{
  public  FB_ShippingAddress ShippingAddressDTO(BC_ShippingAddress bc_ShippingAdd)
  {
    bc_ShippingAdd.first_name = bc_ShippingAdd.first_name.Replace(",", "");
    bc_ShippingAdd.last_name = bc_ShippingAdd.last_name.Replace(",", "");
    bc_ShippingAdd.company = bc_ShippingAdd.company.Replace(",", "");
    bc_ShippingAdd.street_1 = bc_ShippingAdd.street_1.Replace(",", "");
    bc_ShippingAdd.street_2 = bc_ShippingAdd.street_2?.Replace(",", "");
    bc_ShippingAdd.city = bc_ShippingAdd.city.Replace(",", "");
    bc_ShippingAdd.state = bc_ShippingAdd.state.Replace(",", "");
    /*
    bc_ShippingAdd.zipcode = bc_ShippingAdd.zipcode.Replace(",", "");
    */
    bc_ShippingAdd.country = bc_ShippingAdd.country.Replace(",", "");
    bc_ShippingAdd.email = bc_ShippingAdd.email.Replace(",", "");
    bc_ShippingAdd.phone = bc_ShippingAdd.phone.Replace(",", "");
    bc_ShippingAdd.shipping_method = bc_ShippingAdd.shipping_method.Replace(",", "");
    bc_ShippingAdd.cost_ex_tax = bc_ShippingAdd.cost_ex_tax.Replace(",", "");
    
    
    
    Type type = bc_ShippingAdd.GetType();
    
    PropertyInfo[] properties = type.GetProperties();

    foreach (var property in properties)
    {
      Console.WriteLine(property.Name + " : " + property.GetValue(bc_ShippingAdd)?.ToString());
      if (property.PropertyType.Name == "String")
      {
        property.SetValue(bc_ShippingAdd, (property.GetValue(bc_ShippingAdd))?.ToString()?.Replace(",",""));
        Console.WriteLine(property.Name + " : " + property.GetValue(bc_ShippingAdd)?.ToString());

      }
    }
    
    if (bc_ShippingAdd.street_2 != null)
      return new FB_ShippingAddress((bc_ShippingAdd.first_name +" "+ bc_ShippingAdd.last_name),bc_ShippingAdd.street_1,
        bc_ShippingAdd.street_2,bc_ShippingAdd.city, bc_ShippingAdd.state, bc_ShippingAdd.zipcode, bc_ShippingAdd.country); 
    else
      return new FB_ShippingAddress((bc_ShippingAdd.first_name +" "+ bc_ShippingAdd.last_name),bc_ShippingAdd.street_1,
      bc_ShippingAdd.city, bc_ShippingAdd.state, bc_ShippingAdd.zipcode, bc_ShippingAdd.country);
  }
  public FB_BillingAddress BillingAddressDTO(BC_BillingAddress bc_BillingAdd)
  {
    bc_BillingAdd.first_name = bc_BillingAdd.first_name.Replace(",", "");
    bc_BillingAdd.last_name = bc_BillingAdd.last_name.Replace(",", "");
    bc_BillingAdd.company = bc_BillingAdd.company.Replace(",", "");
    bc_BillingAdd.street_1 = bc_BillingAdd.street_1.Replace(",", "");
    bc_BillingAdd.street_2 = bc_BillingAdd.street_2?.Replace(",", "");
    bc_BillingAdd.city = bc_BillingAdd.city.Replace(",", "");
    bc_BillingAdd.state = bc_BillingAdd.state.Replace(",", "");
    bc_BillingAdd.zipcode = bc_BillingAdd.zipcode.Replace(",", "");
    bc_BillingAdd.country = bc_BillingAdd.country.Replace(",", "");
    bc_BillingAdd.email = bc_BillingAdd.email.Replace(",", "");
    bc_BillingAdd.phone = bc_BillingAdd.phone.Replace(",", "");
    
    if(bc_BillingAdd.street_2 != null)
      return new FB_BillingAddress(bc_BillingAdd.first_name + " " + bc_BillingAdd.last_name,
        bc_BillingAdd.street_1, bc_BillingAdd.street_2, bc_BillingAdd.city, bc_BillingAdd.state, bc_BillingAdd.zipcode, bc_BillingAdd.country);
    else 
      return new FB_BillingAddress(bc_BillingAdd.first_name + " " + bc_BillingAdd.last_name,
      bc_BillingAdd.street_1, bc_BillingAdd.city, bc_BillingAdd.state, bc_BillingAdd.zipcode, bc_BillingAdd.country);
    
  }
  public List<FB_SOItem> SOItemListDTO(List<BC_OrderProduct> bc_Products)
  {
    List<FB_SOItem> fb_soItemList = new List<FB_SOItem>();
    
    foreach (BC_OrderProduct product in bc_Products)
    {
      fb_soItemList.Add(new FB_SOItem(FB_SOItem.SOItemType.Sale, product.id.ToString(), product.sku.Replace(",",""),
        product.name.Replace(",",""), Convert.ToDouble(product.base_price), product.quantity,
        "ea", Convert.ToDouble(product.discountAmount)));
    }
    
    return fb_soItemList;
  }
  
  //Create the CSV string needed for the POST request to
  //Fishbowl
  public string CSVOrderDTO(FB_BillingAddress fb_BillingAddress,
    FB_ShippingAddress fb_ShippingAddress,
    BC_Customer bc_Customer, List<FB_SOItem> fb_soItemList, int orderID, string shipping_speed, double shipping_cost)
  {
    List<string> SOHeaderList = new List<string>();
    
    SOHeaderList.Add("SO");
    SOHeaderList.Add($"TEST_{orderID}");
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

    OrderItemList.Add(SOHeaderList.ToArray());

    foreach(FB_SOItem item in fb_soItemList)
    {
      OrderItemList.Add(new string[]{"Item","10", $"{item.ProductNumber}",
        $"{item.ProductDescription}",$"{item.ProductQuantity}",
        $"ea",$"{item.ProductPrice}" });
    }
    OrderItemList.Add(new string[]{"Item","60", "Shipping & Tracking", shipping_speed, "1", "ea", $"{shipping_cost}" });

    return JsonSerializer.Serialize((OrderItemList));
  }
}
