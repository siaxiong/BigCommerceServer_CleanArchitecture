// Ignore Spelling: Fb

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Base;
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Core.Entities.Fishbowl;
using Clean.Architecture.Core.Interfaces;

namespace Clean.Architecture.Core.Services.Fishbowl;
/*public class FishbowlService : IFishbowlService
{
  public FB_ShippingAddress CreateFBShippingAddress(BC_ShippingAddress address) 
  {
    return new FB_ShippingAddress(address.street,address.city,address.state,
      address.zipcode,address.country);
  }

  public FB_BillingAddress CreateFBBillingAddress(BC_BillingAddress address) 
  {
    return new FB_BillingAddress(address.first_name + " " + address.last_name, address.street_1, address.city, address.state,
      address.zipcode, address.country);
    
  }

  public FB_SOItem CreateFBSOItem(BC_OrderProduct item)
  {
    return new FB_SOItem(FB_SOItem.SOItemType.Sale,item.id, item.sku, "Magicore", item.base_price, item.quantity, "EACH", 20);
  }

  public void AddFBSOItem(FB_SO so,FB_SOItem item)
  {
    so.AddFBSOItem(item);
  }

  public FB_SO CreateFBSO(List<FB_SOItem> items, FB_BillingAddress fBBillingAddress,
    FB_ShippingAddress fBShippingAddress)
  {
    return new FB_SO(items, fBBillingAddress, fBShippingAddress);
  }

  public string CreateCSVFBSO(FB_SO fbso)
  {
    return "";
  }

}
*/
