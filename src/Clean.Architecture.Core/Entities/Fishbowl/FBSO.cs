using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.Fishbowl;
public class FBSO
{
  FBBillingAddress _fbillingAddress;
  FBShippingAddress _fbShippingAddress;
  List<FBSOItem> _fbItemList;

  public FBSO(List<FBSOItem> items, FBBillingAddress fbillingAddress, FBShippingAddress fbShippingAddress)
  {
    this._fbItemList = items;
    this._fbillingAddress = fbillingAddress;
    this._fbShippingAddress = fbShippingAddress;
  }

  public void AddFBSOItem(FBSOItem item)
  {
    _fbItemList.Add(item);
  }

  public FBSOItem GetFBSOItem(int id)
  { return _fbItemList[id]; }
}
