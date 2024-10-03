using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Azure.Core;
using Clean.Architecture.Core.Entities.Fishbowl;
using Clean.Architecture.Infrastructure.Data;
using Clean.Architecture.Infrastructure.HttpObjectMapping;
using Microsoft.Extensions.Configuration;

namespace Clean.Architecture.Infrastructure;
public record FB_Credit_Struct {
  public string? name;
  public double? remainingCredits;
  public int? id;
};

public class CustomerCreditStruct
{
  public string? name { get; set; }
  public double balance { get; set; }
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
    var content =
      new StringContent(
        $"{{\"appName\":\"{FB_APPNAME}\",\"appId\":{FB_APPID},\"username\":\"{FB_USERNAME}\",\"password\":\"{FB_PASSWORD}\"}}",
        Encoding.UTF8, "application/json");

    var resp = await _httpClient.PostAsync(_httpClient.BaseAddress + "/login", content);
    var respAsString = await resp.Content.ReadAsStringAsync();
    Http_FB_Login? data = JsonSerializer.Deserialize<Http_FB_Login>(await resp.Content.ReadAsStringAsync(),
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    if (data != null) {
      _httpClient.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", data.token);
      
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
        Console.WriteLine(">>>>>>> CreateLogion() From CreateFBSO()");

        await CreateLogin();
        var resp2 = await _httpClient.PostAsync(_httpClient.BaseAddress + "/import/Sales-Order", content);
        var respString = await resp2.Content.ReadAsStringAsync();
      }
      throw new Exception("Bad api request for CreateFBSO()");
    }
  }
  
  public async Task<List<FB_Credit>> GetAllCustomerCredits()
  {
    List<FB_Credit> companyCredits = new List<FB_Credit>();
    string requestStatusCode = "";
    
    var httpContent = new StringContent(
      "select customer.name, balance   from (\nselect so.customerId, d2.customerId as custId, d2.soId, d2.num,sum(d2.totPrice), sum(d2.totalAmount) as TotalPayment,sum(totalPrice) as TotalOwed,case when sum(d2.totalAmount) is NULL then sum(totalPrice) else sum(totalPrice)-sum(d2.totalAmount) end as balance from so left join (\nselect customerId, d1.soId, num, sum(totalPrice) as totPrice, sum(amount) as totalAmount, sum(totalPrice)-sum(amount) as Balance from \n(select customer.id as customerId, name,so.id as soId, num, totalPrice \nfrom customer right join so on customer.id = so.customerId where so.statusId = 60) d1 right join postransaction\non postransaction.soId = d1.soId group by customerId order by customerId asc ) d2 on so.id = d2.soId where so.statusId = 60 group by so.customerId\norder by d2.customerId desc ) d3 left join customer on customer.id = d3.customerId order by customer.name asc;",
      Encoding.UTF8, "application/sql"
    );
    var requestSetup = new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + "/data-query");
    requestSetup.Headers.Add("Accept", "application/sql");
    requestSetup.Content = httpContent;  

    try
    {
      var request = await _httpClient.SendAsync(requestSetup);
      requestStatusCode = request.StatusCode.ToString();

      var data = JsonSerializer.Deserialize<List<CustomerCreditStruct>>(request.Content.ReadAsStringAsync().Result);

      if (data == null) throw new Exception("null, GetAllCustomerCredits() ");
      foreach (CustomerCreditStruct credits in data)
      {
        List<B2B_Company_Extra_Field> extraFields = new List<B2B_Company_Extra_Field>();
        extraFields.Add(new B2B_Company_Extra_Field{fieldName="Remaining_Credits (Negative # means have money.)", fieldValue=(credits.balance).ToString()});

        if (credits.name == null) throw new Exception("null, GetAllCustomerCredits()");
        companyCredits.Add(new FB_Credit(credits.name, credits.balance));
      }
      return companyCredits;
    }
    catch {
      if (requestStatusCode != "Unauthorized")
      {
        throw new Exception("Bad api request for GetAllCustomerCredits()");
      }
      Console.WriteLine(">>>>>>> CreateLogion() From GetAllCustomerCredits()");
      await CreateLogin();
      var request = await _httpClient.SendAsync(requestSetup);
      var data = JsonSerializer.Deserialize<List<CustomerCreditStruct>>(request.Content.ReadAsStringAsync().Result);

      if (data == null) throw new Exception("null, GetAllCustomerCredits() ");
      foreach (CustomerCreditStruct credits in data)
      {
        List<B2B_Company_Extra_Field> extraFields = new List<B2B_Company_Extra_Field>();
        extraFields.Add(new B2B_Company_Extra_Field{fieldName="Remaining_Credits (Negative # means have money.)", fieldValue=(credits.balance).ToString()});

        if (credits.name == null) throw new Exception("null, GetAllCustomerCredits()");
        companyCredits.Add(new FB_Credit(credits.name, credits.balance));
      }
      return companyCredits;
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
        
        Console.WriteLine(">>>>>>> CreateLogion() From GetCustomerCredit()");
        await CreateLogin();
        
        var request2 = new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + "/data-query");
        request2.Headers.Add("Accept", "application/sql");
        request2.Content = httpContent;
        
        var request = await _httpClient.SendAsync(request2);
        requestStatusCode = request.StatusCode.ToString();
      
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
