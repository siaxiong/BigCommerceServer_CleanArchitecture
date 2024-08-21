using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Clean.Architecture.Infrastructure.HttpObjectMapping;
using Microsoft.Extensions.Configuration;

namespace Clean.Architecture.Infrastructure.BigCommerce;
public class B2C_V2_Context
{
  private readonly HttpClient _httpClient;
  public B2C_V2_Context(HttpClient httpClient, IConfiguration config)
  {
    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri(config["env:B2C_V2_ENDPOINT"]!);
    _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", config["env:B2C_V2_TOKEN"]);
  }
  public async Task<Http_B2C_Order> GetOrder(int orderId)
  {
    var resp = await _httpClient.GetAsync(_httpClient.BaseAddress + "/orders/" + orderId);
    var respString = await resp.Content.ReadAsStringAsync();
    Http_B2C_Order? data = JsonSerializer.Deserialize<Http_B2C_Order>(respString, new JsonSerializerOptions { PropertyNameCaseInsensitive=true});
    if (data != null) return data;
    else throw new Exception("null resp, GetOrder()");
  }
  public async Task<Http_B2C_V2_Customer> GetCustomer(int customerId)
  {
    var resp =  await _httpClient.GetAsync(_httpClient.BaseAddress + "/customers/" + customerId);
    var respString = await resp.Content.ReadAsStringAsync();
    Console.WriteLine(respString);
    Http_B2C_V2_Customer? data = JsonSerializer.Deserialize<Http_B2C_V2_Customer>(respString, new JsonSerializerOptions { PropertyNameCaseInsensitive=true });
    Console.WriteLine(data);
    if (data != null) return data;
    else throw new Exception("null resp, GetCustomer()");
  }
  public async Task<Http_B2C_OrderProduct[]> GetOrderProducts(int orderId)
  {
    var resp = await _httpClient.GetAsync(_httpClient.BaseAddress + "/orders/" + orderId + "/products");
    var respString = await resp.Content.ReadAsStringAsync();
    Console.WriteLine(respString);
    Http_B2C_OrderProduct[]? data = JsonSerializer.Deserialize<Http_B2C_OrderProduct[]>(respString, new JsonSerializerOptions {PropertyNameCaseInsensitive=true});
    Console.WriteLine(respString);

    if (data != null) return data;
    else throw new Exception("null, GetOrderProducts()");
  }
  public async Task<Http_BC_ShippingAddress> GetOrderShippingAddress(int orderId)
  {
    var resp = await _httpClient.GetAsync(_httpClient.BaseAddress + "/orders/" + orderId + "/shipping_addresses");
    var respString = await resp.Content.ReadAsStringAsync();
    Http_BC_ShippingAddress[]? data = JsonSerializer.Deserialize<Http_BC_ShippingAddress[]>(respString, new JsonSerializerOptions {PropertyNameCaseInsensitive=true});
    if (data != null) return data[0];
    else throw new Exception("null resp, GetOrderShippingAddress()");
  }

  public async Task<Http_B2C_V2_Customer> GetCustomerV2(int customerId)
  {
    var resp = await _httpClient.GetAsync(_httpClient.BaseAddress + "/customers/" + customerId);
    var respString = await resp.Content.ReadAsStringAsync();
    var data = JsonSerializer.Deserialize<Http_B2C_V2_Customer>(respString, new JsonSerializerOptions { PropertyNameCaseInsensitive=true});
    if (data != null) return data;
    else throw new Exception("null resp, GetCustomerV2()");
  }
  public async Task<Http_B2C_CustomerGroupName> GetCustomerGroupName(int customerGroupId)
  {
    var resp = await _httpClient.GetAsync(_httpClient.BaseAddress + "/customer_groups/" + customerGroupId);
    var respString = await resp.Content.ReadAsStringAsync();
    Console.WriteLine(respString);
    Http_B2C_CustomerGroupName? data = JsonSerializer.Deserialize<Http_B2C_CustomerGroupName>(respString, new JsonSerializerOptions { PropertyNameCaseInsensitive=true});
    Console.WriteLine(data);
    if (data != null) return data;
    else throw new Exception("null resp, GetCustomerGroupName()");
  }
}
