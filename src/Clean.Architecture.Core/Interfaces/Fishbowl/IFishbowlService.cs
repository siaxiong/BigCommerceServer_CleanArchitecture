using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Base;
using Clean.Architecture.Core.Entities.BC.B2C;
using Clean.Architecture.Core.Entities.Fishbowl;
using Clean.Architecture.Core.Services;

namespace Clean.Architecture.Core.Interfaces.Fishbowl;
public interface IFishbowlService
{
  public FBShippingAddress CreateFBShippingAddress(B2CShippingAddress address);
  public FBBillingAddress CreateFBBillingAddress(B2CBillingAddress address);
  public FBSOItem CreateFBSOItem(B2COrderProduct b2COrderProduct);
/*  public List<FBSOItem> CreateFBSOItemList(FBSOItem soItem);*/
  public FBSO CreateFBSO(List<FBSOItem> items, FBBillingAddress fBBillingAddress,
    FBShippingAddress fBShippingAddress);

}
