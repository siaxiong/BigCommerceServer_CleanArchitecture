using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.BC;

public record BC_OrderProduct(
  int id,
  int order_id,
  int product_id,
  string sku,
  string name,
  int quantity,
  double base_price,
  double? discountAmount,
  string? discountName
  );

/*public class BC_OrderProduct
{
  public int id { get; set; }
  public int order_id { get; set; }
  public int product_id { get; set; }
  public string sku { get; set; }
  public int quantity { get; set; }
  public double base_price { get; set; }


  public BC_OrderProduct(int id, int order_id,
    int product_id, string sku, int quantity, double base_price)
  {
    this.id = id;
    this.order_id = order_id;
    this.product_id = product_id;
    this.sku = sku;
    this.quantity = quantity;
    this.base_price = base_price;
  }
}
*/

