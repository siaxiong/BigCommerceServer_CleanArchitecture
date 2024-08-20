using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Core.Interfaces;
using Microsoft.Extensions.Configuration;


namespace Clean.Architecture.Core.Services.BC;
/*public class B2CService : IBCService
{
  string first_name = "Henry";
  string last_name = "Ford";
  string company = "Ford";
  string street_1 = "7830 W Sahara Ave";
  string city = "Last Vegas";
  string state = "NV";
  string zipcode = "89117";
  string country = "USA";
  string phone = "916.906.5559";
  string email = "henry.ford@ibsimplant.net";

  string store_credit = "$1000";
  string notes = "this is a note";

  private readonly HttpClient _httpClient;

  public B2CService(HttpClient httpClient, IConfiguration config)
  {
    this._httpClient = httpClient;
    _httpClient.BaseAddress = new Uri((config["env:BC_B2C_ENDPOINT_V2"] ?? "na"));
    _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", config["env:BC_B2C_V2_Token"]);
  }
  public BC_BillingAddress GetB2CBillingAddress()
  {
    return new BC_BillingAddress(first_name,last_name,company,street_1,city,state,
      zipcode,country, phone, email);
  }
  public BC_ShippingAddress GetB2CShippingAddress()
  {
    int id = 200;
    int order_id = 123;
    int items_total = 1000;
    string shipping_method = "UPS Ground";
    string cost_ex_tax = "0";

    return new BC_ShippingAddress(id, order_id, first_name, last_name,
      company, street_1, city,zipcode,state,country,email, phone, items_total, shipping_method, cost_ex_tax);
  }
  public async Task<HttpResponseMessage> GetB2COrder(int orderId)
  {
    return await _httpClient.GetAsync(_httpClient.BaseAddress + "/orders/" + orderId);
  }
  public async Task<HttpResponseMessage> GetOrderProducts(int orderId)
  {
    return await _httpClient.GetAsync(_httpClient.BaseAddress + "/orders/" +orderId + "/products");
  }
  public async Task<HttpResponseMessage> GetOrderShippingAddress(int orderId)
  {
    return await _httpClient.GetAsync(_httpClient.BaseAddress + "/orders/" + orderId + "/shipping_addresses");
  }
  public async Task<HttpResponseMessage> GetUserEmail(int userId)
  {
    return await _httpClient.GetAsync(_httpClient.BaseAddress + "/customers?id:in=" + userId);
  }
  public async Task<HttpResponseMessage> GetCustomer(int customerId)
  {
    return await _httpClient.GetAsync(_httpClient.BaseAddress + "/customers/" + customerId);
  }
  public BC_OrderProduct GetB2COrderProduct()
  {
    string id = "999";
    string order_id = "123";
    string product_id = "4545";
    string sku = "451M4511";
    int quantity = 10;
    double base_price = 420;

    return new BC_OrderProduct(id, order_id, product_id, sku, quantity, base_price);
  }
  public BC_Customer GetB2CCustomer()
  {
    return new BC_Customer(4444, company, first_name, last_name,
      email, phone, store_credit, notes);
  }
  public BC_Order CreateB2COrder(int id)
  {
    int id2 = 10;
    int status_id = 1;
    int customer_id = 8890;
    string subtotal_inc_tax = "$100";
    string payment_method = "ACH";
    string payment_status = "Pending";
    string store_credit_amount = "$1000";

    BC_BillingAddress billingAddress = GetB2CBillingAddress();
    BC_ShippingAddress shippingAddress = GetB2CShippingAddress();
    List<BC_OrderProduct> products = new List<BC_OrderProduct>();

    return new BC_Order(id2, status_id, customer_id, subtotal_inc_tax,
      payment_status,payment_method, store_credit_amount, billingAddress,
      shippingAddress, products);
  }
  public BC_ShippingAddress ExtractB2COrderShippingAddress(int orderId)
  {
    return GetB2CShippingAddress();
  }
  public BC_OrderProduct ExtractGetB2COrderProduct (int orderId)
  {
    return GetB2COrderProduct();
  }
  public BC_Customer ExtractB2CCustomer(int customerId)
  {
    return GetB2CCustomer();
  }
}
*/
