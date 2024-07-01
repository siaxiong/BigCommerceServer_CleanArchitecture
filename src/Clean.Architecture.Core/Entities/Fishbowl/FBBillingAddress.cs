using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.Fishbowl;
public class FBBillingAddress
{
  public string? BillToName { get; set; }
  public string? BillToAddress { get; set; }
  public string? BillToCity { get; set; }
  public string? BillToState { get; set; }
  public string? BillToZipCode {  get; set; }
  public string? BillToCountry { get; set; }


}
