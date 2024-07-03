using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.Base;

public abstract class AddressBase
{
  public string street;
  public string? street_2;
  public string city;
  public string state;
  public string zipcode;
  public string country;


  public AddressBase(string street, string city,
    string state, string zipcode, string country)
  {
    this.street = street;
    this.city = city;
    this.state = state;
    this.zipcode = zipcode;
    this.country = country;
  }
  public AddressBase(string street,string street_2, string city,
  string state, string zipcode, string country)
  {
    this.street = street;
    this.street_2 = street_2;
    this.city = city;
    this.state = state;
    this.zipcode = zipcode;
    this.country = country;
  }



  public string Street {
    get { return street; }
    set {  street = value; }
  }

  public string City { get { return city; } set { city = value; } }
  public string State { get { return state; } set { state = value; } }
  public string Zipcode { get { return zipcode; } set { zipcode = value; } }
  public string Country { get { return country; } set { country = value; } }
}
