// Ignore Spelling: Fb

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Base;
using Clean.Architecture.Core.Entities.BC.B2C;
using Clean.Architecture.Core.Entities.Fishbowl;
using Clean.Architecture.Core.Interfaces.Fishbowl;

namespace Clean.Architecture.Core.Services.Fishbowl;
public class FishbowlService : IFishbowlService
{
  public FBShippingAddress CreateFBShippingAddress(B2CShippingAddress address) 
  {
    return new FBShippingAddress(address.street,address.city,address.state,
      address.zipcode,address.country);
  }

  public FBBillingAddress CreateFBBillingAddress(B2CBillingAddress address) 
  {
    return new FBBillingAddress(address.first_name + " " + address.last_name, address.street_1, address.city, address.state,
      address.zipcode, address.country);
    
  }

  public FBSOItem CreateFBSOItem(B2COrderProduct item)
  {
    return new FBSOItem(FBSOItem.SOItemType.Sale,item.id, item.sku, "Magicore", item.base_price, item.quantity, "EACH", 20);
  }

  public void AddFBSOItem(FBSO so,FBSOItem item)
  {
    so.AddFBSOItem(item);
  }

  public FBSO CreateFBSO(List<FBSOItem> items, FBBillingAddress fBBillingAddress,
    FBShippingAddress fBShippingAddress)
  {
    return new FBSO(items, fBBillingAddress, fBShippingAddress);
  }

  public string CreateCSVFBSO(FBSO fbso)
  {
    return "";
  }
}
