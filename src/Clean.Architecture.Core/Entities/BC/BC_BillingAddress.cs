using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.BC;
public class BC_BillingAddress
{
  public string first_name { get; set; }
  public string last_name { get; set; }
  public string company { get; set; }
  public string street_1 { get; set; }
  public string? street_2 { get; set; }
  public string city { get; set; }
  public string state { get; set; }
  public string zipcode { get; set; }
  public string country { get; set; }
  public string phone { get; set; }
  public string email { get; set; }

  public BC_BillingAddress(string first_name, string last_name, string company, string street_1,
  string city, string state, string zipcode, string country, string phone, string email)
  {
    this.first_name = first_name;
    this.last_name = last_name;
    this.company = company;
    this.street_1 = street_1;
    this.city = city;
    this.state = state;
    this.zipcode = zipcode;
    this.country = country;
    this.phone = phone;
    this.email = email;
  }

  public BC_BillingAddress(string first_name, string last_name, string company,
    string street_1,string street_2, string city, string state, string zipcode,
    string country, string phone, string email)
  {
    this.first_name = first_name;
    this.last_name = last_name;
    this.company = company;
    this.street_1 = street_1;
    this.street_2 = street_2;
    this.city = city;
    this.state = state;
    this.zipcode = zipcode;
    this.country = country;
    this.phone = phone;
    this.email = email;
  }
}

