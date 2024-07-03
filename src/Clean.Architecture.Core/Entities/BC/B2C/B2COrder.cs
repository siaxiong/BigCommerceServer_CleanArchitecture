using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Fishbowl;

namespace Clean.Architecture.Core.Entities.BC.B2C;
public class B2COrder
{
  public int Id { get; set; }
  public int status_id {  get; set; }
  public int customer_id { get; set; }
  public string subtotal_inc_tax { get; set; }
  public string payment_method { get; set; }  
  public string payment_status { get; set; }
  public string store_credit_amount {  get; set; }
  public B2CBillingAddress B2CBillingAddress { get; set; }

  public B2COrder(int id, int status_id, int customer_id, string subtotal_inc_tax,
    string payment_status, string store_credit_amount, B2CBillingAddress b2cBillingAddress)
  {
    this.Id = id;
    this.status_id = status_id;
    this.customer_id = customer_id;
    this.subtotal_inc_tax = subtotal_inc_tax;
    this.payment_method = payment_status;
    this.payment_status = payment_status;
    this.store_credit_amount = store_credit_amount;
    this.B2CBillingAddress = b2cBillingAddress;
  }
}
