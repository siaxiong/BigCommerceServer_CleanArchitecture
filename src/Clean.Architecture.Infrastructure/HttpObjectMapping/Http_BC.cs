using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Infrastructure.HttpObjectMapping;

public record Http_BC_BillingAddress(
  string first_name,
  string last_name,
  string company,
  string street_1,
  string? street_2,
  string city,
  string state,
  string zip,
  string country,
  string phone,
  string email
  );
public record Http_BC_ShippingAddress(
  int id,
  int order_id,
  int items_total,
  string base_cost,
  string first_name,
  string last_name,
  string company,
  string street_1,
  string? street_2,
  string city,
  string state,
  string zip,
  string country,
  string phone,
  string email,
  string shipping_method,
  string cost_ex_tax
  );
public record Http_B2C_Order(
  int id,
  string status,
  string payment_status,
  string payment_method,
  string store_credit_amount,
  Http_BC_BillingAddress billing_address,
  int customer_id,
  string customer_message,
  string discount_amount,
  string subtotal_inc_tax,
  string total_inc_tax,
  int status_id,
  string shipping_cost_ex_tax
  );
public record Http_B2C_CustomerGroupName(
  string name
  );
public record Http_B2C_V2_Customer {
  public int id { get; set; }
  public string company { get; set; } = "";
  public string first_name { get; set; } = "";
  public string last_name { get; set;} = "";
  public string email { get; set; } = "";
  public string phone { get; set; } = "";
  public string store_credit { get; set; } = "";
  public string notes { get; set; } = "";
  };
public record Http_B2C_Discount(
  string id,
  string amount,
  string name,
  string target
  );
public record Http_B2C_OrderProduct(
  int id,
  int order_id,
  int product_id,
  string sku,
  string name,
  string base_price,
  int quantity,
  Http_B2C_Discount[] applied_discounts,
  int variant_id
  );
public record Http_B2C_V3_Customer_Payload(
  Http_B2C_V3_Customer[] data
  );
public record Http_B2C_V3_Customer(
  string email,
  string first_name,
  string last_name,
  string company,
  string phone,
  string notes,
  int id,
  int customer_group_id  );
