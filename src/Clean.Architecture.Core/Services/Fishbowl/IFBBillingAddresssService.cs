// Ignore Spelling: Fb

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Services.Fishbowl;
public interface IFBBillingAddressService
{
  string IGetFbBillingAddress();
  string ICreateFbBillingAddress(string addresss);

  bool ICreateFbBilling(bool status);
}
