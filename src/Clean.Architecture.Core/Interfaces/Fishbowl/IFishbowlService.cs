using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Base;
using Clean.Architecture.Core.Entities.BC.B2C;
using Clean.Architecture.Core.Entities.Fishbowl;

namespace Clean.Architecture.Core.Interfaces.Fishbowl;
public interface IFishbowlService
{
  public FBShippingAddress CreateFBShippingAddress(AddressBase address);

/*  public FBBillingAddress CreateFBBillingAddress(AddressBase address);
  public SOItem CreateFBSOItem(B2COrderProduct b2COrderProduct);
  public SOItem CreateFBSOItemList(SOItem soItem);
  public FBSO CreateFBSO(FBShippingAddress fBShippingAddress,
    FBBillingAddress fBBillingAddress, List<SOItem> soItems);
  public SOItem GetSOItem();*/


}
