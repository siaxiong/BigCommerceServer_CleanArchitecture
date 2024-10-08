﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Clean.Architecture.Infrastructure.HttpObjectMapping;
using Microsoft.Extensions.Configuration;

namespace Clean.Architecture.Infrastructure.BigCommerce;
public class B2C_V3_Context
{
  private readonly HttpClient _httpClient;

  public B2C_V3_Context(HttpClient httpClient, IConfiguration config) {
    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("B2C_V3_ENDPOINT")!);
    _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", Environment.GetEnvironmentVariable("B2C_V3_TOKEN"));
    _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
  } 
  public async Task<HttpResponseMessage> GetVariantImage() {
    return await _httpClient.GetAsync(_httpClient.BaseAddress + "/catalog/products?limit=250&include=variants,images");
  }
  public async Task<Http_B2C_V3_Customer> GetCustomer(int userId) {
    var resp = await _httpClient.GetAsync(_httpClient.BaseAddress + "/customers?id:in=" + userId);
    var respString = await resp.Content.ReadAsStringAsync();
    Http_B2C_V3_Customer_Payload? data = JsonSerializer.Deserialize<Http_B2C_V3_Customer_Payload>(respString, new JsonSerializerOptions { PropertyNameCaseInsensitive=true});
    if (data != null) return data.data[0];
    else throw new Exception("resp null,Http_B2C_V3_Customer()");
  }
}
