﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Azure.Core;
using Clean.Architecture.Infrastructure.HttpObjectMapping;
using Microsoft.Extensions.Configuration;

namespace Clean.Architecture.Infrastructure;

public record FB_Credit_Struct {
  public string? name;
  public double? remainingCredits;
  public int? id;
};

public record FB_Credit_Resp {
  public List<FB_Credit_Struct>? credits;
}

public class FishbowlContext {
  private readonly HttpClient _httpClient;
  private readonly IConfiguration _configuration;
  private string FB_APPNAME;
  private int FB_APPID;
  private string FB_USERNAME;
  private string FB_PASSWORD;
  
  string requestStatusCode = "";

  public FishbowlContext(HttpClient httpClient, IConfiguration config) {
    this._httpClient = httpClient;
    this._configuration = config;
    this.FB_APPNAME = Environment.GetEnvironmentVariable("FB_APPNAME")!;
    this.FB_APPID = Convert.ToInt32(Environment.GetEnvironmentVariable("FB_APPID")!);
    this.FB_USERNAME = Environment.GetEnvironmentVariable("FB_USERNAME")!;
    this.FB_PASSWORD = Environment.GetEnvironmentVariable("FB_PASSWORD")!;
    this._httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("FB_ENDPOINT")!);
    
    
    this._httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    this._httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("FB_TOKEN"));
    
  }

  public async Task CreateLogin() {
    var content = new StringContent(
        $"{{\"appName\":\"{FB_APPNAME}\",\"appId\":{FB_APPID},\"username\":\"{FB_USERNAME}\",\"password\":\"{FB_PASSWORD}\"}}",
        Encoding.UTF8, "application/json");

    var resp = await _httpClient.PostAsync(_httpClient.BaseAddress + "/login", content);
    var respAsString = await resp.Content.ReadAsStringAsync();
    Http_FB_Login? data = JsonSerializer.Deserialize<Http_FB_Login>(await resp.Content.ReadAsStringAsync(),
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    if (data != null) {
      Environment.SetEnvironmentVariable("FB_TOKEN", data.token);
    }
    else throw new Exception("Bad api request for fishbowl login");
  }

  public async Task GetUsers() {
    var resp = await _httpClient.GetAsync(_httpClient.BaseAddress + "/users");
    var respAsString = await resp.Content.ReadAsStringAsync();
  }

  public async Task CreateFBSO(string orderString) {
    var content = new StringContent(orderString, Encoding.UTF8, "application/json");

    try {
      var resp = await _httpClient.PostAsync(_httpClient.BaseAddress + "/import/Sales-Order", content);
      requestStatusCode = resp.StatusCode.ToString();
      var respString = await resp.Content.ReadAsStringAsync();
    }
    catch {
      if (requestStatusCode == "Unauthorized") {
        await CreateLogin();
        var resp2 = await _httpClient.PostAsync(_httpClient.BaseAddress + "/import/Sales-Order", content);
        var respString = await resp2.Content.ReadAsStringAsync();
      }
      throw new Exception("Bad api request for CreateFBSO()");
    }
  }

  public async Task<double> GetCustomerCredit(string customerName) {
    var httpContent = new StringContent(
        $"select c.id, c.name, SUM(s.totalPrice) as \"remainingCredits\" from customer c left join so s on c.id = s.customerId where c.name = \"{customerName}\" and (s.statusId=60 or s.statusId=20);",
        Encoding.UTF8, "application/sql"
      );
    
    var request1 = new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + "/data-query");
    request1.Headers.Add("Accept", "application/sql");
    request1.Content = httpContent;
    
    try {
      var request = await _httpClient.SendAsync(request1);
      requestStatusCode = request.StatusCode.ToString();
      
      var jssonResp1 = JsonNode.Parse(await request.Content.ReadAsStringAsync());
      if (jssonResp1?[0]?["remainingCredits"] != null)
        return Convert.ToDouble(jssonResp1?[0]?["remainingCredits"]?.ToJsonString());
      else
        throw new Exception("Bad API call. No data returned.");
    }
    catch {
      if (requestStatusCode == "Unauthorized") {
        await CreateLogin();
        
        var request2 = new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + "/data-query");
        request2.Headers.Add("Accept", "application/sql");
        request2.Content = httpContent;
        
        _httpClient.DefaultRequestHeaders.Authorization =
          new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("FB_TOKEN"));
        var request = await _httpClient.SendAsync(request2);
      
        var jssonResp2 = JsonNode.Parse(await request.Content.ReadAsStringAsync());

        if (jssonResp2?[0]?["remainingCredits"] != null)
          return Convert.ToDouble(jssonResp2?[0]?["remainingCredits"]?.ToJsonString());
        else
          throw new Exception("401 - Unauthorized request");
      }
      throw new Exception("Bad api request");
      
    }
  }
  
}
