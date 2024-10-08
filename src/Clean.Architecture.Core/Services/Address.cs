﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Base;

namespace Clean.Architecture.Core.Services;
public class Address : AddressBase
{
  public Address(string street, string city,
    string state, string zipcode, string country):base(street, city, state, zipcode, country)
  {
  }
  public Address(string street, string street_2, string city,
    string state, string zipcode, string country):base(street,street_2, city,state, zipcode, country)  
  {
  }
}
