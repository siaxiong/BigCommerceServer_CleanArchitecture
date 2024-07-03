using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.BC.B2C;
public class B2COrderProduct
{
  public string id { get; set; }
  public string order_id { get; set; }
  public string product_id {  get; set; }

  public string sku {  get; set; }
  public int quantity { get; set; }
  public string base_price { get; set; }

  public B2COrderProduct(string id, string order_id, string product_id, string sku, int quantity, string base_price)
  { 
    this.id = id;
    this.order_id = order_id;
    this.product_id = product_id;
    this.sku = sku;
    this.quantity = quantity;
    this.base_price = base_price;
  }
}


