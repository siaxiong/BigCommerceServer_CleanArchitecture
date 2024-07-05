using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Base;

namespace Clean.Architecture.Core.Entities.Fishbowl;
public class FBBillingAddress : AddressBase
{
  public string billToName;

  public FBBillingAddress(string billToName, string street, string city,
    string zipcode, string state, string country)
    : base(street, city, zipcode, state, country)
  {
    this.billToName = billToName;
  }


}
