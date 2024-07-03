using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.BC.B2C;
public class B2CCustomer
{
  public int id { get; set; }
  public string company { get; set; }
  public string first_name { get; set; }
  public string last_name { get; set; }
  public string email { get; set; }
  public string phone { get; set; }
  public string store_credit { get; set; }
  public string notes { get; set; }

  public B2CCustomer(int id, string company, string first_name, string last_name,
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

}

