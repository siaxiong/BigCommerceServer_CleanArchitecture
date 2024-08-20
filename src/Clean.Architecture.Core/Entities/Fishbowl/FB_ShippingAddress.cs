using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Base;
using Clean.Architecture.Core.Entities.BC;

namespace Clean.Architecture.Core.Entities.Fishbowl;
public class FB_ShippingAddress : AddressBase
{
  public const string CarrierName = "UPS";
  public const string TaxRateName = "None";
  public string ShipToName;
  public FB_ShippingAddress(string _shipToName, string street, string city, string state, string zipcode, string country)
    :base(street, city, state, zipcode, country)
  {
    this.ShipToName = _shipToName;
  }

  public FB_ShippingAddress(string _shipToName, string street, string street_2, string city, string state, string zipcode,
    string country)
    : base(street,street_2, city, state, zipcode, country)
  {
    this.ShipToName = _shipToName;
  }

  public FB_ShippingAddress(string _shipToName, BC_ShippingAddress b2cShipping)
    : base(b2cShipping.street_1, b2cShipping.city, b2cShipping.state,
      b2cShipping.zipcode, b2cShipping.country)
  {
    this.ShipToName = _shipToName;
  }
}
