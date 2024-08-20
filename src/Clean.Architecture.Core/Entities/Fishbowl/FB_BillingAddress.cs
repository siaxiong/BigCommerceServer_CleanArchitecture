using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Base;

namespace Clean.Architecture.Core.Entities.Fishbowl;
public class FB_BillingAddress : AddressBase
{
  public string billToName;

  public FB_BillingAddress(string billToName, string street, string city,
    string zipcode, string state, string country)
    : base(street, city, zipcode, state, country)
  {
    this.billToName = billToName;
  }

  public FB_BillingAddress(string billToName, string street_1,string street_2, string city,
  string zipcode, string state, string country)
  : base(street_1,street_2, city, zipcode, state, country)
  {
    this.billToName = billToName;
  }


}
