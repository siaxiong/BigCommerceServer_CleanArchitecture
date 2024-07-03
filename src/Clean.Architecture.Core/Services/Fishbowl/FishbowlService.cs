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
  public FBShippingAddress CreateFBShippingAddress(AddressBase address) 
  {
    return new FBShippingAddress(address.street,address.city,address.state,
      address.zipcode,address.country);
  }


/*  public FBShippingAddress CreateFBShippingAddress(AddressBase address) { }
  public SOItem CreateFBSOItem(B2COrderProduct b2COrderProduct) { }
  public SOItem CreateFBSOItemList(SOItem soItem) { }
  public FBSO CreateFBSO(FBShippingAddress fBShippingAddress,
    FBBillingAddress fBBillingAddress, List<FBSO> fBSOs)
  { }*/
}
