using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Base;
using Clean.Architecture.Core.Entities.BC.B2C;

namespace Clean.Architecture.Core.Entities.Fishbowl;
public class FBShippingAddress : AddressBase
{
  public FBShippingAddress(string street, string city, string state, string zipcode, string country)
    :base(street, city, state, zipcode, country)
  {
  }

  public FBShippingAddress(string street, string street_2, string city, string state, string zipcode,
    string country)
    : base(street,street_2, city, state, zipcode, country)
  {
  }

  public FBShippingAddress(B2CShippingAddress b2cShipping)
    : base(b2cShipping.street, b2cShipping.city, b2cShipping.state,
      b2cShipping.zipcode, b2cShipping.country)
  { }
}
