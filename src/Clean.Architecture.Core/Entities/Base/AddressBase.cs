using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.Base;

public abstract class AddressBase
{
  public string street_1;
  public string? street_2;

  public string city;
  public string state;
  public string zipcode;
  public string country;


  public AddressBase(string street_1, string city,
    string state, string zipcode, string country)
  {
    this.street_1 = street_1;
    this.city = city;
    this.state = state;
    this.zipcode = zipcode;
    this.country = country;
  }
  public AddressBase(string str,string street_2, string city,
  string state, string zipcode, string country)
  {
    this.street_1 = str;
    this.street_2 = street_2;
    this.city = city;
    this.state = state;
    this.zipcode = zipcode;
    this.country = country;
  }

/*  public string street_1 {
    get { return street_1; }
    set {  street_1 = value; }
  }*/
}
