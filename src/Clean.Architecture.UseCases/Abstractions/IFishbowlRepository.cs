using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Base;
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Core.Entities.Fishbowl;
using Clean.Architecture.Core.Services;

namespace Clean.Architecture.UseCases.Abstractions;
public interface IFishbowlRespository
{
/*  public Task<string> CreateFishbowlSalesOrder(FB_SO so);
*/
  public Task CreateFBSO(string orderString);
  public Task<double> GetFbCustomerCredit(string customerName);

/*  public FB_ShippingAddress CreateFBShippingAddress(BC_ShippingAddress address);
  public FB_BillingAddress CreateFBBillingAddress(BC_BillingAddress address);
  public FB_SOItem CreateFBSOItem(BC_OrderProduct b2COrderProduct);
  */ /*  public List<FBSOItem> CreateFBSOItemList(FBSOItem soItem);*/ /*
  public FB_SO CreateFBSO(List<FB_SOItem> items, FB_BillingAddress fBBillingAddress,
    FB_ShippingAddress fBShippingAddress);*/

}
