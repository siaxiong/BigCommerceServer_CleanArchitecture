using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Fishbowl;

namespace Clean.Architecture.Core.Entities.BC;
public class BC_Order
{
  public int id { get; set; }
  public int status_id { get; set; }
  public int customer_id { get; set; }
  public string subtotal_inc_tax { get; set; }
  public double shipping_cost_ex_tax { get; set; }
  public string shipping_method { get; set; }
  public string payment_method { get; set; }
  public string payment_status { get; set; }
  public string store_credit_amount { get; set; }
  public BC_BillingAddress billing_address { get; set; }
  public BC_ShippingAddress b2CShippingAddress { get; set; }
  public List<BC_OrderProduct> products { get; set; }

  public BC_Order(int id, int status_id, int customer_id, string subtotal_inc_tax, double shipping_cost_ex_tax, string shipping_method,
    string payment_status, string payment_method, string store_credit_amount, BC_BillingAddress b2cBillingAddress,
    BC_ShippingAddress b2CShippingAddress, List<BC_OrderProduct> products)
  {
    this.id = id;
    this.status_id = status_id;
    this.customer_id = customer_id;
    this.subtotal_inc_tax = subtotal_inc_tax;
    this.shipping_cost_ex_tax = shipping_cost_ex_tax;
    this.shipping_method = shipping_method;
    this.payment_method = payment_method;
    this.payment_status = payment_status;
    this.store_credit_amount = store_credit_amount;
    billing_address = b2cBillingAddress;
    this.b2CShippingAddress = b2CShippingAddress;
    this.products = products;
  }
}
