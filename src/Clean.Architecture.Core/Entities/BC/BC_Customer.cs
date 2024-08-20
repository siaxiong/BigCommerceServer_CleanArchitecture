using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.BC;
public class BC_Customer
{
  public int id { get; set; }
  public string company { get; set; }
  public string first_name { get; set; }
  public string last_name { get; set; }
  public string email { get; set; }
  public string? phone { get; set; } = null;
  public string? store_credit { get; set; } = null;
  public string? notes { get; set; } = null;

  public BC_Customer(int id, string company, string first_name, string last_name,
    string email, string phone, string store_credit, string notes)
  {
    this.id = id;
    this.company = company;
    this.first_name = first_name;
    this.last_name = last_name;
    this.email = email;
    this.phone = phone;
    this.store_credit = store_credit;
    this.notes = notes;
  }

  public BC_Customer(int id, string company, string first_name, string last_name,
  string email, string phone, string notes)
  {
    this.id = id;
    this.company = company;
    this.first_name = first_name;
    this.last_name = last_name;
    this.email = email;
    this.phone = phone;
    this.notes = notes;
  }

}

