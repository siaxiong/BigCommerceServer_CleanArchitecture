using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Base;

namespace Clean.Architecture.Core.Entities.BC.B2C;
public class B2CShippingAddress : AddressBase
{
  public int id { get; set; }
  public int order_id { get; set; }
  public string first_name { get; set; }
  public string last_name { get; set; }
  public string company { get; set; }
  public string email { get; set; }
  public string phone { get; set; }
  public int items_total { get; set; }
  public string shipping_method { get; set; }
  public string cost_ex_tax { get; set; }

  public B2CShippingAddress(int id, int order_id, string first_name, 
    string last_name, string company, string street, string? street_2,
    string city, string zipcode, string state,string country, string email,
    string phone, int items_total, string shipping_method, string cost_ex_tax)
    :base(street, city, state, zipcode, country)
  {
    this.id = id;
    this.order_id = order_id;
    this.first_name = first_name;
    this.last_name = last_name;
    this.company  = company;
    this.email = email;
    this.phone = phone;
    this.items_total = items_total;
    this.shipping_method  = shipping_method;
    this.cost_ex_tax = cost_ex_tax;
  }
}
