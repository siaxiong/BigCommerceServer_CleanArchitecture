using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.Fishbowl;
public class FBShippingAddress
{
  public string? ShipToName { get; set; }
  public string? ShipToAddress {  get; set; }
  public string? ShipToCity { get; set; }
  public string? ShipToState { get; set; }
  public string? ShipToZipCode { get; set; }
  public string? ShipToCountry { get; set; }

}
