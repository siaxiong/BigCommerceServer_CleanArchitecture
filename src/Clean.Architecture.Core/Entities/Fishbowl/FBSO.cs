using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.Fishbowl;
public class FBSO
{
  SOItem _sOItem;
  FBBillingAddress _fbillingAddress;
  FBShippingAddress _fbShippingAddress;

  public FBSO(SOItem sOItem, FBBillingAddress fbillingAddress, FBShippingAddress fbShippingAddress)
  {
    this._sOItem = sOItem;
    this._fbillingAddress = fbillingAddress;
    this._fbShippingAddress = fbShippingAddress;
  }
}
